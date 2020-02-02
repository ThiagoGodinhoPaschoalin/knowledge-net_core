using System;

namespace CoreLib.Extensions
{
    public static class JsonExtension
    {
        /// <summary>
        /// Try SerializeObject Indented;
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="class"></param>
        /// <param name="serialized"></param>
        /// <exception cref="NotSupportedException">When to fail</exception>
        /// <returns>Success?</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static bool TrySerialize<TEntity>(this TEntity @class, out string serialized)
        {
            try
            {
                serialized = @class.Serialize();
                return true;
            }
            catch
            {
                serialized = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// SerializeObject Indented;
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="class"></param>
        /// <exception cref="NotSupportedException">When to fail</exception>
        /// <returns>Serialized Entity</returns>
        public static string Serialize<TEntity>(this TEntity @class)
        {
            try
            {
                if (default(TEntity).Equals(@class))
                {
                    throw new NullReferenceException($"[ Serialize: parameter '{nameof(@class)}' is null ]");
                }

                return Newtonsoft.Json.JsonConvert.SerializeObject(@class, Newtonsoft.Json.Formatting.Indented);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException($"[ SerializeObject Failure! ] [ Type: '{typeof(TEntity)}' ]", ex);
            }
        }


        /// <summary>
        /// Try Deserialize;
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="serialized"></param>
        /// <param name="deserialized"></param>
        /// <exception cref="NotSupportedException">When to fail</exception>
        /// <returns>Success?</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static bool TryDeserialize<TEntity>(this string serialized, out TEntity deserialized)
        {
            try
            {
                deserialized = serialized.Deserialize<TEntity>();
                return true;
            }
            catch
            {
                deserialized = default;
                return false;
            }
        }

        /// <summary>
        /// Deserialize;
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="serialized"></param>
        /// <exception cref="NotSupportedException">When to fail</exception>
        /// <returns>TEntity</returns>
        public static TEntity Deserialize<TEntity>(this string serialized)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(serialized))
                {
                    throw new NullReferenceException($"[ Deserialize: parameter '{nameof(serialized)}' is null ]");
                }

                return Newtonsoft.Json.JsonConvert.DeserializeObject<TEntity>(serialized);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException($"[ DeserializeObject Failure! ] [ Type: '{typeof(TEntity)}' ]", ex);
            }
        }
    }
}