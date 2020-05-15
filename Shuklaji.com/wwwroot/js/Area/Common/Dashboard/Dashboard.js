$(document).ready(function () {
    api.get(app.api.urls.dashboard.getDashboardCount).then(function (data) {
        $('.user-count').text(data.data[0].userCount);
        $('.astro-count').text(data.data[0].astroCount);
        $('.query-count').text(data.data[0].queryCount)
    }).catch(api.errorHandler);
});