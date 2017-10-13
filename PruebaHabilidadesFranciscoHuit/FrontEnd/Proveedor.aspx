<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/PHFMaestro.Master" AutoEventWireup="true" CodeBehind="Proveedor.aspx.cs" Inherits="PruebaHabilidadesFranciscoHuit.FrontEnd.Proveedor" %>
<asp:Content ID="ContenidoUsuarios" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                <h3>Control de Proveedor</h3>
                            </div>
                        </center>
                        <!-- /.box-header -->
                        <!-- form start -->                             
                        <div class="box-body">                                               
                                
                                    <div class="form-group">
                                        <asp:Button ID="btnAgregarNuevoUsuario" runat="server" class="btn btn-primary" OnClick="btnAgregarNuevoUsuario_Click" Text="Agregar Proveedor" />
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
                                                    <p>Esta seguro de eliminar la informacion?  si Elimina este registro todos los productos asociados al proveedor se eliminaran</p>
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
                                        <asp:GridView ID="GrdProveedores" runat="server" class="table table-bordered table-striped"   width="100%" AutoGenerateColumns="false"  DataKeyNames="CodigoProveedor" OnRowCommand="GrdProveedores_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Codigo" ItemStyle-VerticalAlign="Top" ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoProveedor" runat="server" CommandName="CodigoProveedor" Text='<%# Eval("CodigoProveedor") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Descripcion Proveedor " ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescripcionProveedor" runat="server" CommandName="DescripcionProveedor" Text='<%# Eval("DescripcionProveedor") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                                                                                                                
                                                <asp:TemplateField HeaderText="Nombre Empresa" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNombreEmpresa" runat="server" CommandName="NombreEmpresa" Text='<%# Eval("NombreEmpresa") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Encargado" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEncargado" runat="server" CommandName="Encargado" Text='<%# Eval("Encargado") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Telefono" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTelefono" runat="server" CommandName="Telefono" Text='<%# Eval("Telefono") %>'></asp:Label>
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
                                </br>
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
                                <h3>Informacion Proveedor</h3>
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
                                                Nombre Proveedor</label>
                                                <asp:TextBox ID="txtNombreProveedor" runat="server" class="form-control underlined" name="name" TabIndex="1" type="text" value=""></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label" for="name">
                                                Nombre Empresa</label>
                                                <asp:TextBox ID="txtNombreEmpresa" runat="server" class="form-control underlined" name="name" TabIndex="3" type="text" value=""></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label" for="name">
                                                Nombre Encargado
                                                </label>
                                                <asp:TextBox ID="txtNombreEncargado" runat="server" class="form-control underlined" name="name" TabIndex="5" type="text" value="" autocomplete="off" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label" for="name">
                                                Telefono
                                                </label>
                                                <asp:TextBox ID="txtTelefono" runat="server" class="form-control underlined" name="name" TabIndex="2" type="text" value=""></asp:TextBox>
                                            </div>                                            
                                            <div class="form-group">
                                                <label class="control-label" for="name">
                                                Email</label>                                                                                                                      
                                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control underlined" name="name" TabIndex="4" type="text" value=""></asp:TextBox>
                                            </div>                                    
                                            <div class="form-group">
                                                <label class="control-label" for="name">
                                                Direccion</label>
                                                <asp:TextBox ID="txtDireccion" runat="server" class="form-control underlined" name="name" TabIndex="6" type="text" value="" autocomplete="off"></asp:TextBox>
                                            </div>                                                                                        
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-10">
                                            <br>
                                            <br>
                                        
                                            <asp:Button ID="btnGrabar" runat="server" class="btn btn-primary" OnClick="btnGrabar_Click" Text="Guardar" TabIndex="9" />
                                            <asp:Button ID="btnCancelar" runat="server" class="btn btn-secondary" OnClick="btnCancelar_Click" Text="Cancelar" TabIndex="10" />
                                            <br></br>
                                            </br>
                                            </br>
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