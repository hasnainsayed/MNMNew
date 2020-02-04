<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PostCalculation.aspx.cs" Inherits="PostCalculation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper"> <section class="content-header">
        <h1>Post Calculation 
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <%--<li>Report</li>--%>
            <li class="active">Post Calculation </li>
        </ol>
    </section>
   
                                    <section>
                                           <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Post Calculation</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
   
                         <div class="row">
                            
                           <table class="table table-bordered">
                               <tr>
                                   <th></th>
                                    <th>From Date</th>
                                    <th>To Date</th>
                                    <th></th>
                               </tr>
                               <tr>
                                   <td>
                                      Select
                                   </td>
                                   <td>
                                     <asp:TextBox ID="fromDate" CssClass="form-control datepicker" runat="server" ></asp:TextBox></td>
                                   <td>
                                     <asp:TextBox ID="toDate" CssClass="form-control datepicker" runat="server"></asp:TextBox> </td>
                                               
                                   
                           
                                   
                                   <td>
                                       <asp:LinkButton ID="btncal" runat="server" CssClass="btn btn-primary pull-left" OnClick="btncal_Click" ><i class="fa fa-spinner"></i> Calculate</asp:LinkButton>
                                   </td>
                                  
                                   
                               </tr>
                               
                           </table>
                            </div>
                        </div>
                    </div>
                </div>
                                               </div>
                                        
                                    </section>
               



                   

         
            </div>
</asp:Content>

