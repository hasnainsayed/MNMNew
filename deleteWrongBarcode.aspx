<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="deleteWrongBarcode.aspx.cs" Inherits="deleteWrongBarcode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Delete Wrong Barcode
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Delete Wrong Barcode</li>
               
            </ol>
        </section>
     <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>

        
<section class="content">
        <!-- /.box-header -->
        <div class="row">
            <div class="col-xs-12">
               
       
                
                <div class="box">
                  
                    <div class="box-body table-responsive">
                        <div id="divAddAlert" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>

                             
                     
                             <div class="col-md-6">
                                <div class="form-group">
                                     <label>Barcode </label>
                                    <asp:TextBox ID="barcode" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>

                            </div>
                        <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Reason </label>
                                         <asp:TextBox runat="server" CssClass="form-control" ID="reasons"></asp:TextBox>
                                        </div>

                                        </div>

                       

                              <div class="box-body table-responsive ">
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                        <asp:Button ID="btnDeleteBarcode"  class="btn btn-danger pull-right btn-round" runat="server" Text="Delete Barcode" OnClick="btnDeleteBarcode_Click" OnClientClick="return confirm('Do you Want to Delete Barcode?');"/>
                    </div>
                </div>
                  
                    </div>
                    <!-- /.box-body -->
                         
                    </div>
                    <!-- /.box-body -->
                </div>
   
                <!-- /.box -->
            </div>
            <!-- /.col -->
        </div>
   </section>
    </ContentTemplate>
                            </asp:UpdatePanel>
            </div>
</asp:Content>

