<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/PHFMaestro.Master" AutoEventWireup="true" CodeBehind="Producto.aspx.cs" Inherits="PruebaHabilidadesFranciscoHuit.FrontEnd.Producto" %>
<asp:Content ID="ContenidoUsuarios" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <script type="text/javascript" >
                            function onlyNumbersPoint(evt) {
                                var e = event || evt; // for trans-browser compatibility
                                var charCode = e.which || e.keyCode;
                                if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                                    return false;
                                } else {
                                    return true;
                                }
                            }
                        </script>        

    <%--<script type="text/javascript">
$(document).ready(function () {

   $('#btnUploadFile').on('click', function () {

      var data = new FormData();

      var files = $("#fileUpload").get(0).files;

      // Add the uploaded image content to the form data collection
      if (files.length > 0) {
           data.append("UploadedImage", files[0]);
      }

      // Make Ajax request with the contentType = false, and procesDate = false
      var ajaxRequest = $.ajax({
           type: "POST",
           url: "/api/fileupload/uploadfile",
           contentType: false,
           processData: false,
           data: data
           });

      ajaxRequest.done(function (xhr, textStatus) {
                    // Do other operation
             });
   });
});
</script>--%>
<asp:scriptmanager runat="server"></asp:scriptmanager>
<asp:UpdatePanel ID="updPrincipal" UpdateMode="Conditional" runat="server">
<ContentTemplate>     
     
    <asp:MultiView ID="mvlPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwConsultaUsuarios" runat="server">     


              <!-- Main content -->
            <section class="content">
                <div class="row">
                <!-- left column -->                    
                
                <div class="col-md-10 col-md-offset-1">
                
                        <!-- general form elements -->
                    <div class="box box-primary">
                        <center>
                            <div class="box-header with-border">
                                <h3>Control de Producto</h3>
                            </div>
                        </center>
                        <!-- /.box-header -->
                        <!-- form start -->                             
                        <div class="box-body">                                               
                                
                                    <div class="form-group">
                                        <asp:Button ID="btnAgregarNuevoUsuario" runat="server" class="btn btn-primary" OnClick="btnAgregarNuevoUsuario_Click" Text="Agregar Producto" />
                                    </div>
                                
                                <div class="modal fade bd-example-modal-lg" id="modalAdvertencia" role="dialog" data-keyboard="false" data-backdrop="static" >
                                    <div class="modal-dialog modal-ls">                                                                                
                                    <!-- Modal content-->
                                        <div class="modal-content">
                                                                
                                            <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">Eliminar registro</h4>
                                            </div>                                                                                                                         
                                            <div class="modal-body">                                                                     
                                                <div class="box-header">
                                                    <div class="form-group">
                                                        

                                                        <asp:Button ID="btnAceptarReg" runat="server" class="btn btn-primary"  Text="Aceptar" OnClick="btnAceptarReg_Click" />     
                                                        <asp:Button ID="btnCancelarReg" runat="server" class="btn btn-default"  Text="Cancelar" onClick="btnCancelarReg_Click" sdata-dismiss="modal"  />                                                                                                    
                                                    </div>                                                                                                                                                                                  
                                                </div>                                                                                                                               
                                            </div>                                                                                                                                                       
                                        </div>                                                                                                                             
                                     </div>
                                </div>

                                <div class="row">
                                    <div class="box-body">     
                                        <asp:GridView ID="GrdProducto" runat="server" class="table table-bordered table-striped"   width="100%" AutoGenerateColumns="false"  DataKeyNames="CodigoProducto" OnRowCommand="GrdProducto_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Codigo" ItemStyle-VerticalAlign="Top" ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoProducto" runat="server" CommandName="CodigoProducto" Text='<%# Eval("CodigoProducto") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nombre Producto " ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNombreProducto" runat="server" CommandName="NombreProducto" Text='<%# Eval("NombreProducto") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                                                                                                                
                                                <asp:TemplateField HeaderText="Proveedor " ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProveedor" runat="server" CommandName="Proveedor" Text='<%# Eval("DescripcionProveedor") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                                                                                                                
                                                <asp:TemplateField HeaderText="Existencia" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExistencia" runat="server" CommandName="Existencia" Text='<%# Eval("Existencia") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Precio" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrecio" runat="server" CommandName="Precio" Text='<%# Eval("Precio") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                
                                                <asp:TemplateField ItemStyle-VerticalAlign="Top" ItemStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnModificar" runat="server" class="btn btn-success" Text="Modificar" CommandName="btnModificar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-VerticalAlign="Top" ItemStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEliminar" runat="server" class="btn btn-danger" Text="Eliminar" CommandName="btnEliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-VerticalAlign="Top" ItemStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnConsultar" runat="server" class="btn btn-warning" Text="Consultar" CommandName="btnConsultar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
               
                                </div>
                                    <br>
                                </br>
                                    <br>
                                </br>	                 
                            </div>
                        
                        <!-- /.box-body -->
                        </div>
                        <!-- /.box -->
                    </div>
                </div>
                <!--/.col (right) -->
                </div>
                <!-- /.row -->
            </section>
            <!-- /.content -->
        </asp:View>
        <asp:View ID="vwMantenimientoUsuario" runat="server">      
              <!-- Main content -->
            <section class="content">
                <div class="row">
                <!-- left column -->
                <div class="col-md-8 col-md-offset-2">
                    <!-- general form elements -->
                    <div class="box box-primary">
                        <center>
                            <div class="box-header with-border">
                                <h3>Informacion Producto</h3>
                            </div>
                        </center>


                               <div id="modalError" class="modal fade bd-example-modal-lg" data-keyboard="false" role="dialog">
                                    <div class="modal-dialog modal-lg">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button class="close" data-dismiss="modal" type="button">
                                                    ×
                                                </button>
                                                <h4 class="modal-title">Error al actualizar la información</h4>
                                            </div>
                                            <div class="modal-body">
                                                <div class="box-header">
                                                    <div class="form-group">
                                                        <p>
                                                            <%= error %>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                        <!-- /.box-header -->
                        <!-- form start -->                            
                                                                                                     
                        <div class="box-body">                                                                           
                                <div class="row">
                                    <div class="panel-body">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label" for="name">
                                                Proveedor Producto</label>                                                                                                                      
                                                    <asp:DropDownList ID="dpdProveedor" class="form-control underlined" data-live-search="true" TabIndex="1" runat="server"></asp:DropDownList>                                            
                                            </div>                                    
                                            <div class="form-group">
                                                <label class="control-label" for="name">
                                                Nombre Producto</label>
                                                <asp:TextBox ID="txtNombreProducto" runat="server" class="form-control underlined" name="name" TabIndex="3" type="text" value=""></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label" for="name">
                                                Existencia
                                                </label>
                                                <asp:TextBox ID="txtExistencia" runat="server" class="form-control underlined" name="name" onkeypress="return onlyNumbersPoint(this);" TabIndex="5" type="text" value="" ></asp:TextBox>
                                            </div>                                            
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label" for="name">
                                                Descripcion Producto
                                                </label>
                                                <asp:TextBox ID="txtDescripcionProducto" runat="server" class="form-control underlined" name="name" TabIndex="2" type="text" value=""></asp:TextBox>
                                            </div>             
                                            <div class="form-group">
                                                <label class="control-label" for="name">
                                                Descuento                                                    
                                                </label>
                                                <asp:TextBox ID="txtDescuento" runat="server" class="form-control underlined" name="name"  onkeypress="return onlyNumbersPoint(this);"  TabIndex="4" type="text" value=""  ></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label" for="name">
                                                Precio</label>                                                                                                                      
                                                    <asp:TextBox ID="txtPrecio" runat="server" class="form-control underlined" name="name" onkeypress="return onlyNumbersPoint(this);" TabIndex="6" type="text" value=""  ></asp:TextBox>
                                            </div>                                    
                                            <%--<div class="form-group">
                                                <asp:Image ID="imgProducto" src="img/default.png" runat="server" Height="193px" Width="256px" />
                                            </div>                                               
                                            <div>
                                                <label for="fileUpload">Seleccione un archivo luego presione cargar imagen:                                                 
                                                <asp:TextBox id="fileUpload"  runat="server"></asp:TextBox>
                                                <asp:Button id="btnUploadFile" runat="server" Text="Cargar Imagen" onclick="btnUploadFile_Click"/>                                                
                                            </div>--%>
                                             



                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-10">
                                            <br>
                                            <br>
                                        
                                            <asp:Button ID="btnGrabar" runat="server" class="btn btn-primary" OnClick="btnGrabar_Click" Text="Guardar" TabIndex="9" />
                                            <asp:Button ID="btnCancelar" runat="server" class="btn btn-secondary" OnClick="btnCancelar_Click" Text="Cancelar" TabIndex="10" />
                                            <br>
                                            <br>                                            
                                        </div>
                                    </div>
                                </div>                       	                 
                            </div>                                                
                        <!-- /.box-body -->
                        </div>
                        <!-- /.box -->
                </div>
                <!--/.col (right) -->
                </div>
                <!-- /.row -->
            </section>
        </asp:View>
    </asp:MultiView>
</ContentTemplate>                  
</asp:UpdatePanel>   
</asp:Content>