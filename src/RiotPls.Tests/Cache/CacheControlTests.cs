using RiotPls.DataDragon;
using Xunit;

namespace RiotPls.Tests.Cache
{
    public class CacheControlTests
    {
        [Fact(DisplayName = "CacheControl.NoCache does not cache anything.")]
        public void TestNoCache()
        {
            var none = CacheControl<string>.None;
            none.Data = "hello";
            Assert.True(none.IsExpired, "Expected CacheControl<T>.None#IsExpired to be true.");
            Assert.Null(none.Data);
            Assert.True(none.ExpiryPolicy.IsPermanent);
        }

        [Fact(DisplayName = "CacheControl.PermanentCache caches permanently.")]
        public void TestPermanentCache()
        {
            var permanent = CacheControl<string>.Permanent;
            permanent.Data = "hello";
            Assert.False(permanent.IsExpired, "Expected CacheControl<T>.Permanent#IsExpired to be false.");
            Assert.NotNull(permanent.Data);
            Assert.Equal("hello", permanent.Data);
            Assert.True(permanent.ExpiryPolicy.IsPermanent);
            Assert.NotNull(permanent.ExpiryPolicy.LastSetTime);
            Assert.Null(permanent.ExpiryPolicy.CacheExpiry);
        }
    }
}