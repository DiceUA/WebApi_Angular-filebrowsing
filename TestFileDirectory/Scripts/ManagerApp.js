'use strict';

var myApp = angular.module('ManagerApp', [])


myApp.controller('ManagerController', ['$scope', '$http', function ($scope, $http) {
    $http.get("/api/FileLurking/").then(function (response) {
        $scope.myData = response.data;
        $scope.currentPath = $scope.myData[0].Path + '\\';
    });

    $http.get("/api/FilesCalc/").then(function (response) {
        var data = response.data;
        CalculateLength($scope, data);
    });

    $scope.selectDirectory = function (x) {
        console.log("Selected: " + x.Name);

        //Show drives exception
        if (x.Path === "root")
        {
            selectFolder($scope, $http, x.Name);
            return;
        }
        //File click exception
        if (x.Length)
        {
            return;
        }

        //Set dash to current path if needed
        if ($scope.currentPath.lastIndexOf("\\") !== $scope.currentPath.length - 1)
            $scope.currentPath += '\\';

        //Browsing
        if (x.Name === "..")
            selectFolder($scope, $http, x.Parent); //return to parent directory
        else
            selectFolder($scope, $http, $scope.currentPath + x.Name);        
    };

}]);



function selectFolder($scope, $http, path) {
     
    var data = { Path: path };   
    $http.post("api/FileLurking/", data).then(function (response) {
        var data = response.data;
        $scope.myData = data;
        $scope.currentPath = path;
        if ($scope.currentPath.lastIndexOf("\\") !== $scope.currentPath.length - 1)
            $scope.currentPath += '\\';
    });

    $http.post("api/FilesCalc/", data).then(function (response) {
        var data = response.data;
        $scope.calcData = data;        
        CalculateLength($scope, data);
    });

};



function CalculateLength($scope, data) {
    console.log(data);
    $scope.lessThan10 = data.lessThan10;
    $scope.from10to50 = data.from10to50;
    $scope.moreThan100 = data.moreThan100;
};





