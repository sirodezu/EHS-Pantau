var CACHE_NAME = 'EHS-cache-v2';
var virtualDirectori = '';
var urlsToCache = [
    virtualDirectori+'/Pwa',
    virtualDirectori +'/favicon.ico',
    virtualDirectori + '/fontawesome/css/all.min.css',
    virtualDirectori + '/fontawesome/webfonts/fa-solid-900.woff2',
    virtualDirectori + '/lib/bootstrap/dist/css/bootstrap.min.css',
    virtualDirectori +'/lib/kendo/styles/kendo.common.min.css',
    virtualDirectori +'/lib/kendo/styles/kendo.default.min.css',
    virtualDirectori + '/lib/kendo/styles/kendo.default.mobile.min.css',
    virtualDirectori + '/lib/kendo/styles/images/kendoui.woff?v=1.1',
    virtualDirectori +'/lib/kendo/styles/kendo.custom.css',
    virtualDirectori +'/css/site_pwa.css',
    virtualDirectori + '/css/login_pwa.css',
    virtualDirectori +'/lib/jquery/dist/jquery.js',
    virtualDirectori +'/lib/bootstrap/dist/js/bootstrap.bundle.min.js',
    virtualDirectori +'/lib/kendo/js/jszip.min.js',
    virtualDirectori +'/lib/kendo/js/kendo.all.min.js',
    virtualDirectori +'/lib/kendo/js/cultures/kendo.culture.id-ID.min.js',
    virtualDirectori + '/js/site.js',
    virtualDirectori +'/pwa_main.js',
    virtualDirectori +'/manifest.json',
    virtualDirectori + '/img/header_logo.png',
    virtualDirectori + '/img/app_logo_150.png',
    virtualDirectori + '/img/logout.png',
    virtualDirectori + '/img/backweb_login.png',
    virtualDirectori + '/img/backweb_header.png',
    virtualDirectori + '/img/backweb_footer.png',
    virtualDirectori + '/img/icons/icon-72x72.png',
    virtualDirectori + '/img/icons/icon-96x96.png',
    virtualDirectori + '/img/icons/icon-128x128.png',
    virtualDirectori + '/img/icons/icon-144x144.png',
    virtualDirectori + '/img/icons/icon-152x152.png',
    virtualDirectori + '/img/icons/icon-192x192.png',
    virtualDirectori + '/img/icons/icon-384x384.png',
    virtualDirectori + '/img/icons/icon-512x512.png'
];


// Perform install steps
self.addEventListener('install', function (event) {
    event.waitUntil(
        caches.open(CACHE_NAME).then(function (cache) {
            //console.log('Opened cache');
            return cache.addAll(urlsToCache);
        })
    );
});


self.addEventListener('fetch', function (event) {
    event.respondWith(
        caches.match(event.request).then(function (response) {
            // Cache hit - return response
            if (response) {
                return response;
            }
            return fetch(event.request);
        })
    );
});


// on close (delete old cache)
self.addEventListener('activate', function (event) {
    event.waitUntil(
        caches.keys().then(function (cacheNames) {
            return Promise.all(
                cacheNames.filter(function (cacheName) {
                    return cacheName != CACHE_NAME
                }).map(function (cacheName) {
                    return caches.delete(cacheName)
                })
            );
        })
    );
});