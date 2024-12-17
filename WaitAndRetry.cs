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

    }
