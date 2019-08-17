angular.module('App').controller('upLoadController', ['$scope', '$rootScope', '$document', '$cookies', 'GenericApi','$location', function ($scope,$rootScope,  $document, $cookies, GenericApi,$location) {

    $scope.Filtro = {};
    $scope.FilesId = "";

   

    var previewNode = document.querySelector("#template");
    previewNode.id = "";
    var previewTemplate = previewNode.parentNode.innerHTML;
    previewNode.parentNode.removeChild(previewNode);

    var _token = $cookies.get('oAuth_token');
    var _config = GenericApi.GetConfig();


    Dropzone.autoDiscover = false;
    var myDropzone = new Dropzone(document.body, { 
        //url: _config.baseUrl + "api/upload/save/",
        url: _config.baseUrl + 'api/mma/envioarquivo',
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
    //    file.previewElement.querySelector(".start").onclick = function () { myDropzone.enqueueFile(file); };
    });
    
    myDropzone.on("removedfile", function (file) {
        $scope.FilesId = "";
        $scope.$digest();
    });

    // Update the total progress bar
    myDropzone.on("totaluploadprogress", function (progress) {
        //document.querySelector("#total-progress .progress-bar").style.width = progress + "%";
    });

    myDropzone.on("sending", function (file, response) {
        //file.previewElement.querySelector(".start").setAttribute("disabled", "disabled");
    });
    myDropzone.on("success", function (file,response) {
        file.previewElement.querySelector("#downloadok").style.display = "block";
        $scope.FilesId = response;

        var _newFile = {};
        _newFile.Data = file.lastModified;
        _newFile.Name = file.name;
        _newFile.Size = file.size;

        if ($scope.currentAnexo=='Objeto') {
            $scope.Atividade.Objetos.push(_newFile);
        }
        if ($scope.currentAnexo == 'Documento') {
            $scope.Atividade.Documentos.push(_newFile);
        }

        $scope.$digest();
        
    });
    myDropzone.on("error", function (file) {
        file.previewElement.querySelector(".progress").style.display = "none";
        file.previewElement.querySelector("#downloaderror").style.display = "block";
    });

    // Hide the total progress bar when nothing's uploading anymore
    //myDropzone.on("queuecomplete", function (progress) {
    //    document.querySelector(".progress-bar").style.opacity = "0";
    //});

    document.querySelector("#actions .start").onclick = function () {
        var _scac = $document[0].getElementById('txtScac').value;
        myDropzone.options.headers.Scac = _scac;
        myDropzone.options.headers.Tipo = $scope.currentAnexo;
        myDropzone.enqueueFiles(myDropzone.getFilesWithStatus(Dropzone.ADDED));
    };

    document.querySelector("#actions .cancel").onclick = function () {
        $scope.FilesId = "";
        $scope.$digest();
        myDropzone.removeAllFiles(true);
    };
}]);
