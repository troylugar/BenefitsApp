using System;

namespace BenefitsApp.Business.Exceptions
{
  public class DuplicateFoundException : Exception
  {
    public DuplicateFoundException(string message) : base(message)
    {
    }
  }
}