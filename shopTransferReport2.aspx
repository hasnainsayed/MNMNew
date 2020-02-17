<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="shopTransferReport2.aspx.cs" Inherits="shopTransferReport2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper"> <section class="content-header">
        <h1>Shop Transfer Report By Group
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <%--<li>Report</li>--%>
            <li class="active">Reports </li>
        </ol>
    </section>
     <asp:UpdatePanel ID="UpdatePanel20" runat="server">
            <ContentTemplate>
                                    <section>
                                           <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Shop Transfer Report By Group</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
   
                         <div class="row">
                             <asp:Label ID="storelocation" runat="server" Visible="false" ></asp:Label>
                           <table class="table table-bordered">
                               <tr>
                                   <td>
                                       <asp:TextBox ID="frmDate" runat="server" CssClass="form-control form_datetime"></asp:TextBox>
                                   </td>
                                   <td>
                                       <asp:TextBox ID="toDate" runat="server" CssClass="form-control form_datetime"></asp:TextBox>
                                   </td>
                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1"
                                            ControlToValidate="frmDate" Text="Required" Font-Bold="True" ForeColor="red" 
                                            ValidationGroup="go">
                                        </asp:RequiredFieldValidator>
                           
                                   
                                   <td>
                                       <asp:LinkButton ID="GetRpt" runat="server" CssClass="btn btn-sm btn-success pull-right" OnClick="GetRpt_Click" ValidationGroup="go"><i class="fa fa-list"></i> Get Report</asp:LinkButton>
                                   </td>
                                  
                                   
                               </tr>
                               
                           </table>
                            
                                               </div>
                         </div>
                     </div>
                 </div>


                    <div class="box box-primary" id="report" runat="server" >
                      <div class="box box-primary">
                          <h3 style='padding-left:17px;'><asp:Label runat="server" ID="lblrpt"></asp:Label>
                          <asp:LinkButton ID="btnexporttoexcel" runat="server" CssClass="btn btn-success pull-right btn-round" Visible="False" OnClick="btnexporttoexcel_Click"><i class="fa  fa-file-excel-o"></i> Export</asp:LinkButton></h3>
                    <div class="box-body">
                        <div class="row">
                        <div class="box-body table-responsive no-padding">
                            <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                        </div>
                         </div>
                        </div>
                    </div>
            </div>


                             
                                    </section>
               



                   
</ContentTemplate>
         <%--<Triggers>
             <asp:PostBackTrigger ControlID="btnexporttoexcel_Click" />
         </Triggers>--%>
         </asp:UpdatePanel>
            </div>
</asp:Content>

