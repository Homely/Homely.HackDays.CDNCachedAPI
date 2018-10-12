# Homely.HackDays.CDNCachedAPI
Testing out if we can use a CDN (in this case, Fastly) to cache our REST API responses.

Goal: reduce load on server.

# How to test
1. Go here: http://cachetest.hly.bz/api/values/1
2. Refresh your browser. You'll notice time is the same, indicating caching on Fastly. (can also check response headers)
3. Go here: http://cachetest.hly.bz/api/values/1/flush. Cache is now flushed.
4. Go here: http://cachetest.hly.bz/api/values/1. You'll notice item is updated from server (as time is different)

Cool, huh?
