using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calabonga.Microservices.Core.Extensions
{
    public class TaskExtensions
    {
        /// <summary>
        /// Process all tasks and waiting when all will be completed. Exceptions are collected from all tasks too.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tasks"></param>
        public static async Task<IEnumerable<T>> WhenAll<T>(params Task<T>[] tasks)
        {
            var allTasks = Task.WhenAll(tasks);

            try
            {
                return await allTasks;
            }
            catch (Exception)
            {
                // ignore
            }

            throw allTasks.Exception ?? throw new Exception("This can't possible happen!");
        }
    }
}
