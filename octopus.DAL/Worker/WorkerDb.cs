namespace octopus.DAL.WorkerEntities
{
	/// <summary>
	/// Worker database instance
	/// </summary>
	class WorkerDb
	{
		/// <summary>
		/// Database internal short id
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Database connection string
		/// </summary>
		public string ConnectionString { get; set; }
	}
}
