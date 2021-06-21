angular.module('App').controller('TabelaPrecoImportacaoController', ['$scope', '$rootScope', 'httpService', '$location','$cookies', '$document','$timeout', function ($scope, $rootScope, httpService, $location,$cookies,$document, $timeout) {

    //====================Inicializa scopes
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.TipoTabela = [
      { 'id': 'NOR', 'Nome': 'Normal' },
      { 'id': 'MER', 'Nome': 'Merchandising' }
    ]
    $scope.Filtro = "";
    $scope.Precos = [];
    $scope.ShowFilter = true;
    $scope.ShowGrid = false;
    $scope.NewFilter = function () {
        $scope.Filtro = { 'Competencia': '', 'Tipo_Preco': '', 'file': '' }
        $scope.ShowFilter = true;
        $scope.ShowGrid = false;
    }
    $scope.NewFilter();
    //=========================Configura dropzone
    var previewNode = document.querySelector("#template");
    previewNode.id = "";
    var previewTemplate = previewNode.parentNode.innerHTML;
    previewNode.parentNode.removeChild(previewNode);

    var _token = $cookies.get('oAuth_token');
    var _config = httpService.GetConfig();
    Dropzone.autoDiscover = false;

    var myDropzone = new Dropzone(document.body, {
        url: _config.baseUrl + 'api/UploadPreco',
        acceptedFiles: ".csv",
        dictInvalidFileType: "Essa operação permite apenas arquivo tipo CSV",
        dictMaxFilesExceeded:"Somente um Arquivo pode ser Enviado em cada Operação",
        thumbnailWidth: 80,
        thumbnailHeight: 80,
        //parallelUploads: 1,
        maxFiles:1,
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
        //$scope.$apply();
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
        $("#btnLimpar").trigger("click")
        $scope.LerTabela();
    });
    myDropzone.on("error", function (file) {
        file.previewElement.querySelector("#downloaderror").style.display = "block";
    });
    // Hide the total progress bar when nothing's uploading anymore
    myDropzone.on("queuecomplete", function (progress) {
        //document.querySelector(".progress-bar").style.opacity = "0";
        //$("#btnLimpar").trigger("click")
    });

    document.querySelector("#actions .start").onclick = function () {
        myDropzone.enqueueFiles(myDropzone.getFilesWithStatus(Dropzone.ADDED));
    };
    document.querySelector("#actions .cancel").onclick = function () {
        myDropzone.removeAllFiles(true);
    };
    $scope.Validar = function (pFiltro) {
        if (!pFiltro.Competencia) {
            ShowAlert("Competência não foi Informada");
            return;
        };
        if (!pFiltro.Tipo_Preco) {
            ShowAlert("Tipo da Tabela não foi Informada");
            return;
        };
        if (myDropzone.files.length == 0) {
            ShowAlert("Nenhum arquivo CSV foi informado.");
            return;
        };
        pFiltro.file = myDropzone.files[0].name;
    };
    $scope.LerTabela = function (pFiltro) {

        httpService.Post("ImportarTabelaPrecos", $scope.Filtro).then(function (response) {
            if (response.data) {
                $scope.Precos = response.data;
                $scope.ShowFilter = false;
                $scope.ShowGrid = true;
            }
        });
    };
    $scope.ProcessarIntegracao = function (pPreco) {
        httpService.Post("ProcessarImportacaoPreco", pPreco).then(function (response) {
            if (response.data) {
                $scope.Precos = response.data;
                $scope.ShowFilter = false;
                $scope.ShowGrid = true;
            }
        });
    };
}]);

