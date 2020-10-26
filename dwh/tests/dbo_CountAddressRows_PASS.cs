using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tests
{
    [TestClass()]
    public class dbo_CountAddressRows_PASS : SqlDatabaseTestClass
    {

        public dbo_CountAddressRows_PASS()
        {
            InitializeComponent();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            base.InitializeTest();

            //Load test data
            using (var connection = new SqlConnection(this.PrivilegedContext.Connection.ConnectionString))
            {
                DataTable src = new DataTable();

                using (StreamReader sr = new StreamReader("data/addresses.txt"))
                {
                    var header = sr.ReadLine().Split(new string[] { "\t" }, StringSplitOptions.None);
                    foreach (var f in header)
                    {
                        src.Columns.Add(f);
                    }

                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine().Split(new string[] { "\t" }, StringSplitOptions.None);
                        var row = src.NewRow();

                        for (int i = 0; i < line.Length; i++)
                            row[header[i]] = line[i];

                        src.Rows.Add(row);
                    }

                    using (SqlBulkCopy copy = new SqlBulkCopy(connection))
                    {
                        copy.DestinationTableName = "dbo.Address";
                        copy.BatchSize = src.Rows.Count;
                        connection.Open();
                        copy.WriteToServer(src);
                    }
                }
            }
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            base.CleanupTest();
        }

        #region Designer support code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CountAddressRowsTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dbo_CountAddressRows_PASS));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CountAddressRowsTest_PosttestAction;
            this.dbo_CountAddressRowsTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            dbo_CountAddressRowsTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            scalarValueCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_CountAddressRowsTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            // 
            // dbo_CountAddressRowsTest_TestAction
            // 
            dbo_CountAddressRowsTest_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(dbo_CountAddressRowsTest_TestAction, "dbo_CountAddressRowsTest_TestAction");
            // 
            // dbo_CountAddressRowsTestData
            // 
            this.dbo_CountAddressRowsTestData.PosttestAction = dbo_CountAddressRowsTest_PosttestAction;
            this.dbo_CountAddressRowsTestData.PretestAction = null;
            this.dbo_CountAddressRowsTestData.TestAction = dbo_CountAddressRowsTest_TestAction;
            // 
            // scalarValueCondition1
            // 
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = "450";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;
            // 
            // dbo_CountAddressRowsTest_PosttestAction
            // 
            resources.ApplyResources(dbo_CountAddressRowsTest_PosttestAction, "dbo_CountAddressRowsTest_PosttestAction");
        }

        #endregion


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion

        [TestMethod()]
        public void dbo_CountAddressRowsTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_CountAddressRowsTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        private SqlDatabaseTestActions dbo_CountAddressRowsTestData;
    }
}
