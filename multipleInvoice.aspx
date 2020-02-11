<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="multipleInvoice.aspx.cs" Inherits="multipleInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">
        <section class="content-header">
            <h1>Print Multiple Invoice</h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="invoice.aspx">Invoice</a></li>
                <li class="active">Print Multiple Invoice</li>
            </ol>
        </section>
        <section class="content">
            <!-- Default box -->
            <div class="box">
                <div class="box-body">
                    <!-- BOx -->
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Print Multiple Invoice</h3>

                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive">
                            <div class="row">
                                <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                    <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>


                                        <div class="col-md-12">
                                            <div class="col-md-12">
                                                <asp:Button ID="dropdownAdd" runat="server" Text="Add More" class="btn bg-purple btn-round margin btn-sm" OnClick="dropdownAdd_Click" />
                                            </div>
                                            <table class="table table-bordered table-striped">
                                                <thead>
                                                    <tr>
                                                        <th>Invoice No</th>
                                                       
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rpt_dropdown" runat="server" OnItemDataBound="rpt_dropdown_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>                                                                  
                                                                    <asp:DropDownList ID="drp_dropdown" CssClass="form-control select2" runat="server" DataTextField="invName" DataValueField="invid"></asp:DropDownList>
                                                                </td>                                                               
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>


                                        <div class="col-md-12">
                                            <div class="box-body table-responsive ">
                                                <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                  <asp:LinkButton runat="server" ID="printInvoices" OnClick="printInvoices_Click" CssClass="btn btn-sm btn-success"><i class="fa fa-print"></i> Print</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                   
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>

