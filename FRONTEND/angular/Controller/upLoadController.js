angular.module('App').controller('upLoadController', ['$scope', '$rootScope', '$document', '$cookies', 'httpService', '$location', function ($scope, $rootScope, $document, $cookies,httpService,  $location) {

    var previewNode = document.querySelector("#template");
    previewNode.id = "";
    var previewTemplate = previewNode.parentNode.innerHTML;
    previewNode.parentNode.removeChild(previewNode);

    var _token = $cookies.get('oAuth_token');
    var _config = httpService.GetConfig();
    Dropzone.autoDiscover = false;

    var myDropzone = new Dropzone(document.body, {
        url: _config.baseUrl + 'api/RetornPlayListUpload',
        //acceptedFiles: "",
        //dictInvalidFileType: "Essa operação permite apenas arquivo tipo texto",
        //dictMaxFilesExceeded:"Somente um Arquivo pode ser Enviado em cada Operação",
        thumbnailWidth: 80,
        thumbnailHeight: 80,
        parallelUploads: 20,
        //maxFiles:1,
        previewTemplate: previewTemplate,
        autoQueue: false,
        previewsContainer: "#previews",
        clickable: ".fileinput-button",
        headers: { "Authorization": 'Bearer ' + _token },
    });

    myDropzone.on("addedfile", function (file) {
        //file.previewElement.querySelector(".start").onclick = function () { myDropzone.enqueueFile(file); };
    });

    myDropzone.on("removedfile", function (file) {

        $scope.$apply();
    });

    // Update the total progress bar
    myDropzone.on("totaluploadprogress", function (progress) {
        //document.querySelector("#total-progress .progress-bar").style.width = progress + "%";
    });

    myDropzone.on("sending", function (file, response) {
        //file.previewElement.querySelector(".start").setAttribute("disabled", "disabled");
    });
    myDropzone.on("success", function (file, response) {
        file.previewElement.querySelector("#downloadok").style.display = "block";
        file.previewElement.querySelector(".progress").style.opacity = "0";
        //var _newFile = {};
        //_newFile.Data = file.lastModified;
        //_newFile.Name = file.name;
        //_newFile.Size = file.size;
        $scope.AddAnexo(file.name);
        $scope.$apply();
    });
    myDropzone.on("error", function (file) {
        file.previewElement.querySelector("#downloaderror").style.display = "block";
    });

    // Hide the total progress bar when nothing's uploading anymore
    myDropzone.on("queuecomplete", function (progress) {
        //document.querySelector(".progress-bar").style.opacity = "0";
        $("#btnLimpar").trigger("click")
    });

    document.querySelector("#actions .start").onclick = function () {
        var _Id_Upload = $scope.Id_Upload;
        myDropzone.options.headers.Id_Upload = _Id_Upload;
        myDropzone.enqueueFiles(myDropzone.getFilesWithStatus(Dropzone.ADDED));
    };

    document.querySelector("#actions .cancel").onclick = function () {
        myDropzone.removeAllFiles(true);
    };
}]);
