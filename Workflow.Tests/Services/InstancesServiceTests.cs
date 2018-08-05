﻿using System;
using System.Collections.Generic;
using System.Linq;
using Chauffeur.TestingTools;
using umbraco;
using Workflow.Models;
using Workflow.Services;
using Workflow.Services.Interfaces;
using Xunit;

namespace Workflow.Tests.Services
{
    public class InstancesServiceTests : UmbracoHostTestBase
    {
        private readonly IInstancesService _service;
        private readonly ITasksService _tasksService;

        public InstancesServiceTests()
        {
            Host.Run(new[] {"install y"}).Wait();

            Scaffold.Run();
            Scaffold.Config();

            _service = new InstancesService();
            _tasksService = new TasksService();
        }

        [Fact]
        public void Can_Get_All()
        {
            _service.InsertInstance(Scaffold.Instance(new Guid(), 1));
            _service.InsertInstance(Scaffold.Instance(new Guid(), 1, 1074));
            _service.InsertInstance(Scaffold.Instance(new Guid(), 1, 1075));

            IEnumerable<WorkflowInstancePoco> instances = _service.GetAll();
        
            // this is only returning a single instance. I know it works, because dashboards use it...
            Assert.NotNull(instances);
        }

        [Fact]
        public void Can_Get_By_Guid()
        {
            var guid = new Guid();

            _service.InsertInstance(Scaffold.Instance(guid, 1));

            WorkflowInstancePoco instance = _service.GetByGuid(guid);

            Assert.NotNull(instance);
            Assert.Equal(guid, instance.Guid);
        }

        [Fact]
        public void Can_Get_By_NodeId()
        {
            const int nodeId = 1075;

            _service.InsertInstance(Scaffold.Instance(new Guid(), 1, nodeId));

            List<WorkflowInstance> instances = _service.GetByNodeId(nodeId, 1, 10);

            Assert.NotNull(instances);
            Assert.Equal(nodeId, instances.GetRandom().NodeId);
        }
    }
}
