//viewModel = {
//    jobCollection: ko.observableArray()
//};

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
$('input#city').cityAutocomplete();
});

