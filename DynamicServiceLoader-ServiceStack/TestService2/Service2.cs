
using DynamicServiceLoader.BaseClasses;
using ServiceStack.ServiceInterface.ServiceModel;
using System;

namespace TestService2
{
  public class Service2 : MyServiceBase
  {
    public Service2Response Get(Service2Request request)
    {
      var response = new Service2Response() { ResponseStatus = new ResponseStatus() };

      float operationResult = 0f;

      try
      {
        switch (request.Operation.ToLowerInvariant())
        {
          case "add":
            operationResult = request.Num1 + request.Num2;
            break;
          case "multiply":
            operationResult = request.Num1 * request.Num2;
            break;
          case "subtract":
            operationResult = request.Num1 - request.Num2;
            break;
          case "divide":
            operationResult = request.Num1 / request.Num2;
            break;
          default:
            throw new ArgumentException("No valid operation found for passed argument.");
        }

        response.OperationResult = operationResult;

      }
      catch (Exception ex)
      {
        response.ResponseStatus = new ResponseStatus("-1", ex.Message);
      }

      return response;
    }
  }


  public class Service2Response : IHasResponseStatus
  {
    public float OperationResult { get; set; }
    public ResponseStatus ResponseStatus { get; set; }
  }


  public class Service2Request
  {
    public string Operation { get; set; }
    public float Num1 { get; set; }
    public float Num2 { get; set; }
  }
}
