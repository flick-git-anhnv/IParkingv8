namespace Kztek.Control8
{
    public static class ControlUiInvokeExtensions
    {
        public static Task<T> UIInvokeAsync<T>(this Control c, Func<T> func)
        {
            if (!c.InvokeRequired) return Task.FromResult(func());
            var tcs = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
            c.BeginInvoke(new Action(() =>
            {
                try { tcs.TrySetResult(func()); } catch (Exception ex) { tcs.TrySetException(ex); }
            }));
            return tcs.Task;
        }

        public static Task UIInvokeAsync(this Control c, Action action)
        {
            if (!c.InvokeRequired) { action(); return Task.CompletedTask; }
            var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
            c.BeginInvoke(new Action(() =>
            {
                try { action(); tcs.TrySetResult(null); } catch (Exception ex) { tcs.TrySetException(ex); }
            }));
            return tcs.Task;
        }

        public static Task<T> UIInvokeAsync<T>(this Control c, Func<Task<T>> funcAsync)
        {
            var tcs = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
            c.BeginInvoke(new Action(async () =>
            {
                try { var r = await funcAsync().ConfigureAwait(false); tcs.TrySetResult(r); }
                catch (Exception ex) { tcs.TrySetException(ex); }
            }));
            return tcs.Task;
        }
    }

}
