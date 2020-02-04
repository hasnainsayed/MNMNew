<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="packaging.aspx.cs" Inherits="packaging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Packaging</h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Report</li>
                <li class="active"><a  href="styleSale.aspx">Packaging</a></li>
               
            </ol>
        </section>
   <asp:UpdatePanel runat="server" ID="uPanel1">
       <ContentTemplate>
<section class="content">
        <!-- /.box-header -->
        <div class="row">
            <div class="col-xs-12">
               
            
               
                <div class="box">
                  
                    <div class="box-body table-responsive">
<table class="table table-bordered table-stripped">

                                            <tr>
                                               <th><asp:CheckBox runat="server" ID="dateRange" Checked="true"/> Date Range</th>
                                               <td><asp:TextBox ID="fromDate" CssClass="form-control datepicker" runat="server" AutoPostBack="true"></asp:TextBox></td>
                                               <td><asp:TextBox ID="toDate" CssClass="form-control datepicker" runat="server"></asp:TextBox></td>
                                                
                                           </tr>
                                          <tr>
                                               <th><asp:CheckBox runat="server" ID="barcodeCheck"/> Barcode</th>
                                               <td colspan="2"><asp:TextBox ID="barcode" runat="server" CssClass="form-control"></asp:TextBox></td>
                                           </tr>
                                           <tr>                                              
                                               <th><asp:CheckBox runat="server" ID="vLocCheck"/> Packaging Location</th>
                                               <td><asp:DropDownList ID="vLoc" runat="server" CssClass="form-control select2" DataTextField="rackcode" DataValueField="rackcode"></asp:DropDownList></td>
                                               <td><asp:LinkButton ID="searchPackaging" runat="server" OnClick="searchPackaging_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                                   
                                               </td>
                                           </tr>
                                          
                                           
                                          
                                       </table>

                    </div>
                     
                                    
                             <table class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>Barcode </th>   
                                            <th>DateTime</th>
                                            <th>RackCode</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                     
                                        <asp:Repeater ID="rpt_package" runat="server">
                                            <ItemTemplate>
                                               <tr>
                                                   <td><%# Eval("packBarcode").ToString() %></td>
                                                   <td><%# Eval("entryDate").ToString() %></td>
                                                   <td><%# Eval("rackcode").ToString() %></td>
                                               </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </tbody>
                                </table>

             

                      
                         
                    </div>
                    <!-- /.box-body -->
                </div>
   
                <!-- /.box -->
            </div>
         
   </section>
   </ContentTemplate>
   </asp:UpdatePanel>
            </div>
</asp:Content>

