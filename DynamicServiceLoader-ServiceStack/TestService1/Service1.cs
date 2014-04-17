
using DynamicServiceLoader.BaseClasses;
using ServiceStack.ServiceInterface.ServiceModel;

namespace TestService1
{
  public class Service1 : MyServiceBase
  {

    public Service1Response Get(Service1Request request)
    {
      var response = new Service1Response() { ResponseStatus = new ResponseStatus(), Greeting = string.Format("Hello, Thanks for your message: {0}", request.Text) };
      return response;
    }

  }

  public class Service1Response : IHasResponseStatus
  {
    public ResponseStatus ResponseStatus { get; set; }
    public string Greeting { get; set; }
  }

  public class Service1Request
  {
    public string Text { get; set; }
  }
}
