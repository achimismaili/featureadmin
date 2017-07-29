using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Services.SharePointApi
{
    /// <summary>
    /// The singleton for the SharePoint in memory database
    /// </summary>
    /// <remarks>
    /// see also http://www.laserbrain.se/2015/11/async-singleton-initialization/
    /// </remarks>
    public class SingletonDb
    {
        private static readonly Lazy<SingletonDb> LazySingleton =
        new Lazy<SingletonDb>(CreateSingleton);

        public static SingletonDb Singleton
        {
            get { return LazySingleton.Value; }
        }

        /// <summary>
        /// The public available singleton instance.
        /// </summary>
        public static Task<SingletonDb> SingletonAsync { get; } = CreateSingletonAsync();

        //private static readonly Task<SingletonDb> CreateSingletonTask =
        //        CreateSingletonAsync();

        //public static Task<SingletonDb> SingletonAsync
        //{
        //    get { return CreateSingletonTask; }
        //}

        /// <summary>
        /// The private instance-constructor, taking some data.
        /// </summary>
        private SingletonDb(InMemoryDataBase db)
        {
            InMemoryDb = db;
        }

        /// <summary>
        /// Some data that the singleton exposes.
        /// </summary>
        public InMemoryDataBase InMemoryDb
        { get; }

        /// <summary>
        /// Create the singleton instance.
        /// </summary>
        private static SingletonDb CreateSingleton()
        {
            InMemoryDataBase db = CreateDb();
            return new SingletonDb(db);
        }

        /// <summary>
        /// Create the singleton instance.
        /// </summary>
        private static async Task<SingletonDb> CreateSingletonAsync()
        {
            InMemoryDataBase db = await CreateDbAsync();
            return new SingletonDb(db);
        }

        private static InMemoryDataBase CreateDb()
        {
            return new InMemoryDataBase();
        }

        /// <summary>
        /// A private method that can create some data, called once.
        /// </summary>
        private static async Task<InMemoryDataBase> CreateDbAsync()
        {
            var db = await Task.Run<InMemoryDataBase>(() =>
            {
                return CreateDb();
            });

            return db;
        }
    }


}
