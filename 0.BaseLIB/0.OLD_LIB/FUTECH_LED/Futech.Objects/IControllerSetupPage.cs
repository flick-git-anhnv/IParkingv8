using System;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public interface IControllerSetupPage
    {

        string ControllerName { get; set;}

        int Address { get; set;}

        int ControllerTypeID { get; set;}

        int Reader1Type { get; set;}

        int Reader2Type { get; set;}

        bool TimeAttendance { get; set;}

        string Description { get; set;}

        Line CurrentLine { set;}

        ControllerTypeCollection ControllerTypes { set;}

        // all controller in line
        ControllerCollection Controllers { set;}
    }
}
