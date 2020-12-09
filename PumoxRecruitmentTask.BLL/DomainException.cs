using System;

namespace PumoxRecruitmentTask.BLL
{
    public class DomainException : Exception
    {
        private DomainExceptionCode ExceptionCode { get; set; }
        public string Description { get; set; }

        public DomainException(DomainExceptionCode exceptionCode, string description = "")
        {
            ExceptionCode = exceptionCode;
            Description = description;
        }

        public static void ThrowIf(bool condition, DomainExceptionCode domainExceptionCode, string description = "")
        {
            if (condition)
            {
                throw new DomainException(domainExceptionCode, description);
            }
        }
    }
}