<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="barcodeStatus.aspx.cs" Inherits="barcodeStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style>
        .card-header {
    background-color:#3c8dbc!important;

}
        .btn-link{color:white;}
        .btn-link:hover {color:white;}
        .btn-link:focus {color:white;}
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper"> <section class="content-header">
        <h1>Barcode History
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li>Reports</li>
            <li class="active">Barcode History</li>
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
                        <h3 class="box-title">Barcode History</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                             <asp:Panel ID="Panel3" runat="server" DefaultButton="barcodeHistory">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                           <table class="table table-bordered">
                               <tr>
                                     <td>
                                         <asp:TextBox ID="barcodeNo" runat="server" CssClass="form-control"></asp:TextBox>
                                     </td>
                                   <td>
                                       <asp:LinkButton ID="barcodeHistory" runat="server" CssClass="btn btn-sm btn-warning" OnClick="barcodeHistory_Click"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                   </td>
                                  
                               </tr>
                       
                           </table>
                                     </ContentTemplate></asp:UpdatePanel>
                        </asp:Panel>
                            </div>
                        <div runat="server" id="showData" visible="false" class=" table-responsive">

                            <div class="accordion" id="accordionExample" runat="server">
                                <div class="card">
                                            <div class="card-header" id="headingOne">
                                                <h5 class="mb-0">
                                                    <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                                        Style History 
                                                    </button>
                                                </h5>
                                            </div>
                                            <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#accordionExample">
                                                <div class="card-body">
                                                     
                                                    <table class="table table-bordered table-stripped">
                                <thead>
                                <tr>
                                    
                                    <th>User</th>
                                    <th>Action</th>
                                    <th>DateTime</th>
                                    
                                   
                                </tr>
                                    </thead>
                                <tbody>
                                 <asp:Repeater ID="rpt_Style" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           
                                           <td><%# Eval("User").ToString() %></td>
                                           <td><%# Eval("Details").ToString() %></td>
                                           <td><%# Eval("Dets").ToString() %></td>
                                           
                                       </tr>
                                       </ItemTemplate>
                                </asp:Repeater>
                                    </tbody>
                            </table>
                                                </div>
                                            </div>
                                        </div>

                                <div class="card">
                                            <div class="card-header" id="headingTwo">
                                                <h5 class="mb-0">
                                                    <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                                        Barcode Status History
                                                    </button>
                                                </h5>
                                            </div>
                                            <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionExample">
                                                <div class="card-body">
                                                    <table class="table table-bordered table-stripped dtSearchDesc">
                                <thead>
                                <tr>
                                    <th>User</th>
                                    <th>Status</th>
                                    <th>DateTime</th>
                                   
                                </tr>
                                    </thead>
                                <tbody>
                                 <asp:Repeater ID="rtp_List" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           <td><%# Eval("User").ToString() %></td>
                                           <td><%# Eval("Status").ToString() %></td>
                                           <td><%# Convert.ToDateTime(Eval("DateTime")).ToString("dd MMM yyyy HH:mm:ss tt") %></td>
                                           
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
                        </div>
                    </div>
                </div>
                                               </div>
                                        
                                    </section>
                                    </ContentTemplate>
             
               </asp:UpdatePanel>
        </div>
</asp:Content>

