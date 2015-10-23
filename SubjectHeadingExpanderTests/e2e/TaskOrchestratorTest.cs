using NUnit.Framework;
using SubjectHeadingExpander;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectHeadingExpanderTests
{
    public class TaskOrchestratorTest
    {
        [Test]
        public void OrchestrateTasksSuccessfully()
        {
            string subjectPrefix = "Filosofi";

            // GIVEN a valid TaskOrchestrator
            TaskOrchestrator taskOrchestrator = new TaskOrchestrator(subjectPrefix);

            // WHEN a subject prefix expansion is performed end to end
             bool result = taskOrchestrator.PerformExpansion();

            // THEN the expansion should succeed
            Assert.IsTrue(result);
        }
    }
}
