using System;
using Microsoft.SqlServer.Dts.Runtime;

namespace MSSSQL.DIARY.SERVICE
{
    class MyEventListener : DefaultEvents
    {
        public override bool OnError(DtsObject source, int errorCode, string subComponent,
            string description, string helpFile, int helpContext, string idofInterfaceWithError)
        {
            // Add application-specific diagnostics here.  
            Console.WriteLine("Error in {0}/{1} : {2}", source, subComponent, description);
            return false;
        }
        public override void OnBreakpointHit(IDTSBreakpointSite breakpointSite, BreakpointTarget breakpointTarget)
        {
            base.OnBreakpointHit(breakpointSite, breakpointTarget);
        }
        public override void OnCustomEvent(TaskHost taskHost, string eventName, string eventText, ref object[] arguments, string subComponent, ref bool fireAgain)
        {
            base.OnCustomEvent(taskHost, eventName, eventText, ref arguments, subComponent, ref fireAgain);
        }
        public override void OnExecutionStatusChanged(Executable exec, DTSExecStatus newStatus, ref bool fireAgain)
        {
            base.OnExecutionStatusChanged(exec, newStatus, ref fireAgain);
        }
        public override void OnInformation(DtsObject source, int informationCode, string subComponent, string description, string helpFile, int helpContext, string idofInterfaceWithError, ref bool fireAgain)
        {
            base.OnInformation(source, informationCode, subComponent, description, helpFile, helpContext, idofInterfaceWithError, ref fireAgain);
        }
        public override void OnPostExecute(Executable exec, ref bool fireAgain)
        {
            base.OnPostExecute(exec, ref fireAgain);
        }
        public override void OnPostValidate(Executable exec, ref bool fireAgain)
        {
            base.OnPostValidate(exec, ref fireAgain);
        }
        public override void OnPreExecute(Executable exec, ref bool fireAgain)
        {
            base.OnPreExecute(exec, ref fireAgain);
        }
        public override void OnPreValidate(Executable exec, ref bool fireAgain)
        {
            base.OnPreValidate(exec, ref fireAgain);
        }
        public override void OnProgress(TaskHost taskHost, string progressDescription, int percentComplete, int progressCountLow, int progressCountHigh, string subComponent, ref bool fireAgain)
        {
            base.OnProgress(taskHost, progressDescription, percentComplete, progressCountLow, progressCountHigh, subComponent, ref fireAgain);
        }
        public override void OnTaskFailed(TaskHost taskHost)
        {
            base.OnTaskFailed(taskHost);
        }
        public override bool OnQueryCancel()
        {
            return base.OnQueryCancel();
        }
        public override void OnVariableValueChanged(DtsContainer DtsContainer, Variable variable, ref bool fireAgain)
        {
            base.OnVariableValueChanged(DtsContainer, variable, ref fireAgain);
        }
        public override void OnWarning(DtsObject source, int warningCode, string subComponent, string description, string helpFile, int helpContext, string idofInterfaceWithError)
        {
            base.OnWarning(source, warningCode, subComponent, description, helpFile, helpContext, idofInterfaceWithError);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}