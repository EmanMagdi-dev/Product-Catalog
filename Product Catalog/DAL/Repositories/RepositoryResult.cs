namespace Product_Catalog.DAL.Repositories
{
    public class RepositoryResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public static RepositoryResult<T> Success(T data) => new RepositoryResult<T> { IsSuccess = true, Data = data };

        public static RepositoryResult<T> Failure(string errorMessage) => new RepositoryResult<T> { IsSuccess = false, ErrorMessage = errorMessage };
    }

}
