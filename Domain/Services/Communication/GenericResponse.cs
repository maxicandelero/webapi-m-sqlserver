namespace webapi_m_sqlserver.Domain.Services.Communication
{
    public class GenericResponse<T> where T : class
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public T Entity { get; protected set; }

        private GenericResponse(bool success, string message, T entity) 
        {
            Success = success;
            Message = message;
            Entity = entity;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="entity">Saved entity.</param>
        /// <returns>Response.</returns>
        public GenericResponse(T entity) : this(true, string.Empty, entity)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public GenericResponse(string message) : this(false, message, null)
        { }
    }
}