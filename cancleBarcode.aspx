<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="cancleBarcode.aspx.cs" Inherits="cancleBarcode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Delete Barcode
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Delete Barcode</li>
               
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

                             
                     <asp:Panel ID="Panel3" runat="server" DefaultButton="btnCancelBarcode">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                             <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label1">Barcode</asp:Label>
                                    <asp:TextBox ID="barcode" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>

                            </div>
<div class="col-md-6">
                                        <div class="form-group">
                                            <label>Reason </label>
                                         <asp:TextBox runat="server" CssClass="form-control" ID="reasons"></asp:TextBox>
                                        </div>

                                        </div>
                           
                    <div class="col-md-6" style="margin-top:19px;" ><div class="form-group">
                        <asp:Button ID="btnCancelBarcode"  class="btn btn-danger pull-left btn-round" runat="server" Text="Delete Barcode" OnClick="btnCancelBarcode_Click" OnClientClick="return confirm('Do you Want to Delete Barcode?');"/>
                   <asp:LinkButton runat="server" ID="reprintBarcode" class="btn btn-warning pull-left btn-round" OnClick="reprintBarcode_Click">RePrint Barcode</asp:LinkButton>
                        </div> </div>
            
                  </ContentTemplate></asp:UpdatePanel>
                                        </asp:Panel>
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

