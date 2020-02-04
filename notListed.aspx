<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="notListed.aspx.cs" Inherits="notListed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper"> <section class="content-header">
        <h1>Not Listed SKU
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li>Reports</li>
            <li class="active">Not Listed SKU</li>
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
                        <h3 class="box-title">Not Listed SKU</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                           <table class="table table-bordered">
                               <tr>
                                   <td>
                                       <asp:DropDownList ID="virtualLocation" AutoPostBack="true" runat="server" CssClass="form-control select2" DataTextField="Location" DataValueField="LocationID"></asp:DropDownList>
                                   </td>
                                   <td>
                                       <asp:LinkButton ID="searchSKU" runat="server" CssClass="btn btn-sm btn-warning" OnClick="searchSKU_Click"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                   </td>
                                   <td><asp:LinkButton ID="exportSKUNotListed" runat="server" CssClass="btn btn-sm btn-danger" OnClick="exportSKUNotListed_Click"><i class="fa fa-file"></i> Export</asp:LinkButton></td>
                                   
                               </tr>
                      
                           </table>
                            </div>
                        <div runat="server" id="showData" visible="false" class=" table-responsive">
                            <table class="table table-bordered table-stripped dtSearch">
                                <thead>
                                <tr>
                                    <th>SKU</th>
                                    <th>Title</th>
                                    <th>Category</th>
                                    <th>Brand</th>
                                    <th>MRP</th>
                                </tr>
                                    </thead>
                                <tbody>
                                 <asp:Repeater ID="rtp_List" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           <td><%# Eval("sku").ToString() %></td>
                                           <td><%# Eval("Title").ToString() %></td>
                                           <td><%# Eval("ItemCategory").ToString() %></td>
                                           <td><%# Eval("C1Name").ToString() %></td>
                                           <td><%# Eval("mrp").ToString() %></td>
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
               <Triggers>

            <asp:PostBackTrigger ControlID="exportSKUNotListed" />
        </Triggers>
               </asp:UpdatePanel>
        </div>
</asp:Content>

