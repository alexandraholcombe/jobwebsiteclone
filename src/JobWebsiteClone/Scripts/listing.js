//viewModel = {
//    jobCollection: ko.observableArray()
//};

var defaultBounds = new google.maps.LatLngBounds(
  new google.maps.LatLng(-90, -180),
  new google.maps.LatLng(90, 180));

var input = document.getElementById('city');
var options = {
    bounds: defaultBounds,
    types: ['(cities)']
};

var searchBox = new google.maps.places.SearchBox(input, {
    bounds: defaultBounds
});

$(document).ready(function () {
    //$.ajax({
    //    type: "GET",
    //    url: "Job/Search",
    //}).done(function (data) {
    //    $(data).each(function (index, element) {
    //        viewModel.jobCollection.push(element);
    //    });
    //    ko.applyBindings(viewModel);
    //}).error(function (ex) {
    //    alert("Error");
    //});
});

