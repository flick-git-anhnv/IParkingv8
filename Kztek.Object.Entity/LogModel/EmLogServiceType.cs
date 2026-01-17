namespace Kztek.Object
{
    public enum EmLogServiceType
    {
        OFFLINE_FILE,
        OFFLINE_DB,
    }
    public enum EmSystemAction
    {
        Application,
        MainServer,
        MainServer_Sumary,
        ThirtParty,
        MessageQueue,
        SOCKET,
        DEVICE,
        USER
    }

    public enum EmSystemActionType
    {
        INFO,
        WARNING,
        ERROR,
    }

    public enum EmSystemActionDetail
    {
        GET,
        CREATE,
        DELETE,
        UPDATE,
        PROCESS,
        LOOP_EVENT,
        CARD_EVENT,
        EXIT_EVENT,
        CARD_BE_TAKEN
    }
}
