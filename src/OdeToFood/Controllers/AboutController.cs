using Microsoft.AspNet.Mvc;

namespace OdeToFood.Controllers
{
    //public class AboutController
    //{
    //    public string Phone()
    //    {
    //        return "314.825.8305";
    //    }

    //    public string Country()
    //    {
    //        return "USA";
    //    }
    //}

    [Route("company/[controller]/[action]")]
    public class AboutController
    {
        //[Route("")]
        public string Phone()
        {
            return "314.825.8305";
        }

        //[Route("[action]")]
        public string Country()
        {
            return "USA";
        }
    }
}
