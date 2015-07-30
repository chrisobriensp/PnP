(function () {
    'use strict';

    angular
        .module('app.wizard')
        .service('$SharePointProvisioningService', function ($q, $http) {
            this.getSiteTemplates = function ($scope) {
                var deferred = $.Deferred();

                $http({
                    method: 'GET',
                    url: '/Provisioning.UX.AppWeb/api/provisioning/templates/getAvailableTemplates',
                    headers:
                    {
                        'accept': 'application/json'
                    }
                }).success(function (data, status, headers, config) {
                    console.debug("Request Success /Provisioning.UX.AppWeb/api/provisioning/templates/getAvailableTemplates", data);
                    deferred.resolve(data.templates)
                }).error(function (data, status) {
                    deferred.reject(data);
                });
                return deferred;
            }
            this.saveNewSiteRequest = function (request) {
                var deferred = $q.defer();
                var formData = JSON.stringify(request);
                $http({
                    method: 'POST',
                    url: '/Provisioning.UX.AppWeb/api/provisioning/siteRequests/newSiteRequest',
                    data: "=" + formData,
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).success(function (data, status, headers, config) {
                    console.debug("Request Success /Provisioning.UX.AppWeb/api/provisioning/siteRequests/newSiteRequest ", data);
                    deferred.resolve(data);
                }).error(function (data, status) {
                    console.log("Request Failed /Provisioning.UX.AppWeb/api/provisioning/newSiteRequest Request " + data);
                    deferred.reject(data);
                });
                return deferred;
            }
            this.getSiteRequestsByOwners = function (request) {
                var deferred = $.Deferred();
                var formData = JSON.stringify(request);
                $http({
                    method: 'POST',
                    data: "=" + formData,
                    url: '/Provisioning.UX.AppWeb/api/provisioning/siteRequests/getOwnerRequests',
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).success(function (data, status, headers, config) {
                    console.log("Request Success /Provisioning.UX.AppWeb/api/provisioning/getOwnerRequests " + data);
                    deferred.resolve(data);
                }).error(function (data, status) {
                    console.log("Request Failed /Provisioning.UX.AppWeb/api/provisioning/getOwnerRequests " + data);
                    deferred.reject(data);
                });
                return deferred;
            }
            this.isExternalSharingEnabled = function (request) {
                var deferred = $.Deferred();
                var formData = JSON.stringify(request);
                $http({
                    method: 'POST',
                    data: "=" + formData,
                    url: '/Provisioning.UX.AppWeb/api/provisioning/externalSharingEnabled',
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).success(function (data, status, headers, config) {
                    deferred.resolve(data);
                    console.log("Request Succssess to Provisioning.UX.AppWeb/api/provisioning/externalSharingEnabled result is " + data);
                }).error(function (data, status) {
                    deferred.reject(data);
                    console.log("Request Failed to Provisioning.UX.AppWeb/api/provisioning/externalSharingEnabled " + data );
                });
                return deferred;
            }
            this.doesSiteRequestExist = function (request) {
                var deferred = $.Deferred();
                var formData = JSON.stringify(request);
                $http({
                    method: 'POST',
                    data: "=" + formData,
                    url: '/Provisioning.UX.AppWeb/api/provisioning/siteRequests/validateNewSiteRequestUrl',
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).success(function (data, status, headers, config) {
                    deferred.resolve(data);
                    console.log("Request Succssess to Provisioning.UX.AppWeb/api/provisioning/validateNewSiteRequestUrl result is " + data);
                }).error(function (data, status) {
                    deferred.reject(data);
                    console.log("Request Failed to Provisioning.UX.AppWeb/api/provisioning/validateNewSiteRequestUrl " + data);
                });
                return deferred;
            }
        });
})();
