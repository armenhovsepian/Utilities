    public static class WaitAndRetry
    {

        public static bool Execute(Func<bool> func, int maxRetries, Action onSuccess = null, Action onRetryFailed = null, Action<Exception> onMaxRetriesFailed = null)
        {
            var retries = maxRetries;
            var successed = false;
            while (!successed)
            {
                try
                {
                    var result = func();
                    if (result)
                    {
                        successed = true;
                        if (onSuccess != null)
                        {
                            onSuccess();
                        }

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    if (retries >= 0)
                    {
                        WebDriverExtensions.Pause(1000);

                        retries--;
                        if (onRetryFailed != null)
                        {
                            onRetryFailed();
                        }
                    }
                    else
                    {
                        if (onMaxRetriesFailed != null)
                        {
                            onMaxRetriesFailed(ex);
                        }
                        else
                        {
                            throw new MaxRetriesExceededException("Reached the max retries!", ex);
                        }
                    }
                }
            }

            return false;
        }

        public static async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> func, int maxRetries, Action onSuccess = null, Action onRetryFailed = null, Action<Exception> onMaxRetriesFailed = null)
        {
            var retries = maxRetries;
            var successed = false;
            while (!successed)
            {
                try
                {
                    var result = await func();

                    successed = true;
                    if (onSuccess != null)
                    {
                        onSuccess();
                    }

                    return result;

                }
                catch (Exception ex)
                {
                    if (retries >= 0)
                    {
                        WebDriverExtensions.Pause(1000);

                        retries--;
                        if (onRetryFailed != null)
                        {
                            onRetryFailed();
                        }
                    }
                    else
                    {
                        if (onMaxRetriesFailed != null)
                        {
                            onMaxRetriesFailed(ex);
                        }
                        else
                        {
                            throw new MaxRetriesExceededException("Reached the max retries!", ex);
                        }
                    }
                }
            }

            return default;
        }

        public static async Task ExecuteAsync(Func<Task> func, int maxRetries, Action onSuccess = null, Action onRetryFailed = null, Action<Exception> onMaxRetriesFailed = null)
        {
            var retries = maxRetries;
            var successed = false;
            while (!successed)
            {
                try
                {
                    var result = func();

                    successed = true;
                    if (onSuccess != null)
                    {
                        onSuccess();
                    }

                }
                catch (Exception ex)
                {
                    if (retries >= 0)
                    {
                        WebDriverExtensions.Pause(1000);

                        retries--;
                        if (onRetryFailed != null)
                        {
                            onRetryFailed();
                        }
                    }
                    else
                    {
                        if (onMaxRetriesFailed != null)
                        {
                            onMaxRetriesFailed(ex);
                        }
                        else
                        {
                            throw new MaxRetriesExceededException("Reached the max retries!", ex);
                        }
                    }
                }
            }
        }

        public static void Execute(Action action, int maxRetries, Action onSuccess = null, Action onRetryFailed = null, Action<Exception> onMaxRetriesFailed = null)
        {
            var retries = maxRetries;
            var successed = false;
            while (!successed)
            {
                try
                {
                    action();
                    successed = true;
                    if (onSuccess != null)
                    {
                        onSuccess();
                    }

                }
                catch (Exception ex)
                {
                    if (retries >= 0)
                    {
                        WebDriverExtensions.Pause(1000);

                        retries--;
                        if (onRetryFailed != null)
                        {
                            onRetryFailed();
                        }
                    }
                    else
                    {
                        if (onMaxRetriesFailed != null)
                        {
                            onMaxRetriesFailed(ex);
                        }
                        else
                        {
                            throw new MaxRetriesExceededException("Reached the max retries!", ex);
                        }
                    }
                }
            }
        }



        public static class WaitAndRetry
        {
            public static async Task<T> ExecuteAsync<T>(
                Func<Task<T>> operation,
                Func<T, bool> retryCondition, // Condition to check if retry is needed based on result
                int maxRetries,
                TimeSpan initialDelay,
                Func<int, TimeSpan, TimeSpan> delayStrategy = null) // Optional: custom delay strategy
            {
                delayStrategy ??= (attempt, delay) => delay; // Default to fixed delay

                for (int attempt = 0; attempt <= maxRetries; attempt++)
                {
                    try
                    {
                        T result = await operation();
                        if (!retryCondition(result))
                        {
                            return result; // Operation successful, or result met expectations
                        }
                        // If retryCondition is true, it means the result requires a retry
                        Console.WriteLine($"Attempt {attempt + 1}: Result requires retry. Retrying in {initialDelay}...");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Attempt {attempt + 1}: Operation failed with exception: {ex.Message}. Retrying in {initialDelay}...");
                        // Log the exception if needed
                    }

                    if (attempt < maxRetries)
                    {
                        await Task.Delay(delayStrategy(attempt, initialDelay));
                    }
                }

                // If we reach here, max retries have been exhausted, and the desired result was not achieved
                Console.WriteLine($"Max retries ({maxRetries}) exhausted. Expected result not achieved.");
                throw new MaxRetriesExceededException("The operation did not yield the expected result after the maximum number of retries.");
            }

            public static async Task ExecuteAsync(
                Func<Task> operation,
                int maxRetries,
                TimeSpan initialDelay,
                Func<int, TimeSpan, TimeSpan> delayStrategy = null)
            {
                await ExecuteAsync(async () =>
                {
                    await operation();
                    return true; // Simple operation, always "succeeds" if no exception
                },
                (result) => false, // Never retry based on result for void operations
                maxRetries,
                initialDelay,
                delayStrategy);
            }
            
        }

        public class MaxRetriesExceededException : Exception
        {
            public MaxRetriesExceededException(string message) : base(message) { }
        }

    }
