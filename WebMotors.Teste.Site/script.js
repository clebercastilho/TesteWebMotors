// Extension methods
String.format = function () {
    var s = arguments[0];
    for (var i = 1; i < arguments.length; i++) {
        var r = new RegExp("\\{" + (i - 1) + "\\}", "gm");
        s = s.replace(r, arguments[i]);
    }
    return s;
};

function service(htmlMethod, url, data) {
    var promise = $.ajax({
        type: htmlMethod,
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(data)
    });
    
    return promise;
}

(function (_window, $, undefined) {
    var template = {
        tableRow: '<tr role="row"><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>' +
            '<button type="button" class="btn btn-warning btn-xs edit-anuncio" data-id="{4}" style="margin-right:10px;">Editar</button>' +
            '<button type="button" class="btn btn-danger btn-xs delete-anuncio" data-id="{4}" style="margin-right:10px;">Remover</button>' +
            '</td></tr>'
    };

    var elements = {
        table: $('#anuncios-lista'),
        panelList: $('#panel-list'),
        panelEdit: $('#panel-editing'),
        panelTitle: $('#title-panel-editing'),
        formEdit: $('#form-anuncio'),
        editID: $('#hdnAnuncioID'),
        editMarca: $('#txtMarca'),
        editModelo: $('#txtModelo'),
        editVersao: $('#txtVersao'),
        editAno: $('#txtAno'),
        editQuilometragem: $('#txtKm'),
        editObservacao: $('#txtObs'),
        btnSave: $('#saveEdit'),
        btnCancel: $('#cancelEdit'),
        btnNovo: $('#novoEdit'),
        reset: function() {
            this.editID.val('0');
            this.editMarca.val('');
            this.editModelo.val('');
            this.editVersao.val('');
            this.editAno.val('');
            this.editQuilometragem.val('');
            this.editObservacao.val('');
        }
    };

    function obterAnuncios() {
        $("#anuncios-lista > tbody").html("");
        service('GET', 'http://localhost:63456/api/anuncios', {}).then(function(response) {
            if(response && response.dados) {
                for (var i = 0; i < response.dados.length; i++) {
                    var data = response.dados[i];
                    var row = String.format(template.tableRow, data.marca, data.modelo, data.versao, data.ano, data.id);
                    elements.table.append(row);
                }  
                
                bindActions();
            }
        });
    }

    function bindActions() {
        $('.edit-anuncio').off('click').on('click', editarAnuncio);
        $('.delete-anuncio').off('click').on('click', removerAnuncio);
    }

    function init() {
        elements.panelEdit.hide();
        obterAnuncios();
    }

    //eventos
    elements.btnNovo.click(function () {
        elements.panelList.fadeOut(300, function() {
            elements.panelTitle.html('Criando novo anúncio');
            elements.panelEdit.fadeIn();
        });
    });

    elements.btnCancel.click(function () {
        elements.panelEdit.fadeOut(300, function() {
            elements.panelList.fadeIn();
            elements.reset();
        });
    });

    elements.btnSave.click(function (e) {
        var model = {
            marca: elements.editMarca.val(),
            modelo: elements.editModelo.val(),
            versao: elements.editVersao.val(),
            ano: elements.editAno.val(),
            quilometragem: elements.editQuilometragem.val(),
            observacao: elements.editObservacao.val()
        };

        var ID = elements.editID.val();
        var method = 'POST'

        if(ID != 0){
            method = 'PUT';
            model.id = ID;
        }

        service(method, 'http://localhost:63456/api/anuncios', model).then(function(response) {
            if(response && response.dados) {
                elements.panelEdit.fadeOut(300, function(){
                    elements.reset();
                    obterAnuncios();
                    elements.panelList.fadeIn();
                });
            }
        })
    });

    function editarAnuncio(e) {
        var handler = $(this);
        var id = handler.data('id');

        service('GET', 'http://localhost:63456/api/anuncios/' + id, {}).then(function(response) {
            if(response && response.dados) {
                elements.editID.val(response.dados.id);
                elements.editMarca.val(response.dados.marca);
                elements.editModelo.val(response.dados.modelo);
                elements.editVersao.val(response.dados.versao);
                elements.editAno.val(response.dados.ano);
                elements.editQuilometragem.val(response.dados.quilometragem);
                elements.editObservacao.val(response.dados.observacao);

                elements.panelList.fadeOut(300, function() {
                    elements.panelTitle.html('Editando anúncio');
                    elements.panelEdit.fadeIn();
                });
            }
        });
    }

    function removerAnuncio(e) {
        var handler = $(this);
        var id = handler.data('id');

        service('DELETE', 'http://localhost:63456/api/anuncios/' + id, {}).then(function(response) {
            if(response && response.dados) {
                obterAnuncios();
            }
        });
    }

    init();
})(window, jQuery);