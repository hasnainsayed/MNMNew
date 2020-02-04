<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="barcodeDetails.aspx.cs" Inherits="barcodeDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="content-wrapper"> <section class="content-header">
        <h1>Barcode Details
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li>Reports</li>
            <li class="active">Barcode Details</li>
        </ol>
    </section>
           <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <section>
                                           <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Barcode Details</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                           <table class="table table-bordered">
                               <tr>
                                   <td>
                                       <asp:TextBox ID="barcode" runat="server" CssClass="form-control"></asp:TextBox>
                                   </td>
                                   <td>
                                       <asp:LinkButton ID="searchDetails" runat="server" CssClass="btn btn-sm btn-warning" OnClick="searchDetails_Click"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                   </td>
                                   
                               </tr>
                           </table>
                            </div>
                        <div runat="server" id="showData" visible="false" class=" table-responsive">
                            <table class="table table-bordered table-stripped dtSearch">
                                <thead>
                                <tr>
                                    <th>UserName</th>
                                    <th>DateTime</th>
                                    <th>Status</th>
                                   
                                </tr>
                                    </thead>
                                <tbody>
                                 <asp:Repeater ID="rtp_List" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           <td><%# Eval("username").ToString() %></td>
                                           <td><%# Eval("dates").ToString() %></td>
                                           <td><%# Eval("status").ToString() %></td>
                                          
                                       </tr>
                                       </ItemTemplate>
                                </asp:Repeater>
                                    </tbody>
                            </table>
                       
                        </div>
                        </div>
                    </div>
                </div>
                                               </div>
                                        
                                    </section>
                                    </ContentTemplate>
               </asp:UpdatePanel>
        </div>
</asp:Content>

