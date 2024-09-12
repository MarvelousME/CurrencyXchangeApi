
namespace CurrencyApiInfrastructure.BaseInterfaces.Pagination
{
    public interface IPaginationDto
    {
        public int Skip { get; set; }

        public int Take { get; set; }
    }
}
