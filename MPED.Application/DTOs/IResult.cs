namespace MPED.Application.DTOs
{
    public interface IResult
    {
        string Message { get;set;}
        
        bool Succeeded { get; set; }
    }
}
