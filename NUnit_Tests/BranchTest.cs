using COMP4941_Term_Project;
using COMP4941_Term_Project.Controllers;
using COMP4941_Term_Project.Models;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Assert = NUnit.Framework.Assert;

namespace NUnit_Tests
{
    [TestFixture]
    public class BranchTest
    {
        BranchesController controller = new BranchesController();

        [Test]
        public void BranchCreateTestSuccess()
        {
            var result = controller.Create(new Branch()
            {
                //ID = new Guid("12345678-1234-1234-1234-123456789012") ,
                Name = "name",
                Street = "street",
                City = "city",
                Province = "province",
                Country = "country",
                PostalCode = "postal",
                Phone = "123-456-7890",
                Fax = "987-654-3210",
                Email = "email@email.com",
                Website = "web"
            }) as ActionResult;

            Console.WriteLine(result);

            Assert.That(result is RedirectToRouteResult);

        }

        [Test]
        public void BranchCreateTestFail()
        {
            var caughtError = false;
            try
            {
                ActionResult result = controller.Create(new Branch()
                {
                    Name = "I am missing many required fields and this branch name is too long"
                }) as ActionResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                caughtError = true;

            }
            Assert.That(caughtError == true);
        }

        [Test]
        public void BranchReadIndexTest()
        {
            var result = controller.Index() as ViewResult;
            Console.WriteLine(result.ViewName);

            Assert.That(result.ViewName == "Index");

        }

        private COMP4941_Term_Project.AppContext db = new COMP4941_Term_Project.AppContext();
        [Test]
        public void BranchReadDetailsTest()
        {
            Branch branch = db.Branches.FirstOrDefault();
            Console.WriteLine(branch.ID);

            var result = controller.Details(branch.ID) as ViewResult;
            Console.WriteLine(result.ViewName);

            Assert.That(result.ViewName == "Details");

        }

        [Test]
        public void BranchUpdateTest()
        {
            Branch branch = db.Branches.FirstOrDefault();
            Console.WriteLine(branch);

            var result = controller.Edit(new Branch() 
            {
                ID = branch.ID,
                Name = "new name",
                Street = "street",
                City = "city",
                Province = "province",
                Country = "country",
                PostalCode = "postal",
                Phone = "123-456-7890",
                Fax = "987-654-3210",
                Email = "email@email.com",
                Website = "web"
            }) as ActionResult;

            Assert.That(result is RedirectToRouteResult);
        }

        [Test]
        public void BranchDeleteTest()
        {
            Branch branch = db.Branches.FirstOrDefault();
            Console.WriteLine(branch);

            var result = controller.DeleteConfirmed(branch.ID) as ActionResult;
            Console.WriteLine(result);

            Assert.That(result is RedirectToRouteResult);
        }



    }

}
