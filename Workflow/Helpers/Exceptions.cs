﻿using System;

namespace Workflow.Helpers
{
    internal class WorkflowException : Exception
    {
        public WorkflowException(string message) : base(message) { }
    }    
}
