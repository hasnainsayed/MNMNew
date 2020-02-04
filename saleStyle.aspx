<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="saleStyle.aspx.cs" Inherits="saleStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Items on Sale</h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Website</li>
                <li class="active"><a  href="saleStyle.aspx">Items on Sale</a></li>
               
            </ol>
        </section>
   
<section class="content">
        <!-- /.box-header -->
        <div class="row">
            <div class="col-xs-12">
               
            
                
                <div class="box">
                    <div class="box-header">
                       
                          </div>
                    <div class="box-body table-responsive">
                        <div id="divAddAlert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Updated Succesfully</h5>

                                    </div>

                                    <div id="divUpdAlert" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Update Failed</h5>

                                    </div>

                        <div class="col-md-12">
                                        <asp:Button ID="dropdownAdd" runat="server" Text="Add New" class="btn bg-purple btn-round margin btn-sm" OnClick="dropdownAdd_Click" />
                                    </div>
                             <table class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>Style</th>                                            
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rpt_Style" runat="server" OnItemDataBound="rpt_Style_ItemDataBound">
                                                <ItemTemplate>
                                                <tr>
                                                    <td> 
                                                        <asp:Label runat="server" ID="styleId" Text='<%# Eval("styleId").ToString() %>' Visible="false"></asp:Label>
                                                        <asp:DropDownList ID="style_drop" CssClass="form-control select2" runat="server" DataTextField="Title" DataValueField="StyleID" AutoPostBack="true"></asp:DropDownList>
                                                    </td>
                                                   
                                                </tr>
                                       </ItemTemplate></asp:Repeater>
                                    </tbody>
                                </table>

             

                        <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-round" runat="server" Text="Save Sale Product" OnClick="btnSave_Click" />
                        
                    </div>
                         
                    </div>
                    <!-- /.box-body -->
                </div>
   
                <!-- /.box -->
            </div>
            <!-- /.col -->
        </div>
   </section>
   
            </div>
</asp:Content>

