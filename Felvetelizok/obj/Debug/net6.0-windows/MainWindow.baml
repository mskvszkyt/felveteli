   M S B A M L   `   `   `  ����G  CFelvetelizok, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FelvetelizokFelvetelizok  5   	       Felvetelizok.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Felvetelizok     5   	     � Title$  Kifir ��5   	   $	��700��5	      $
��1100��6   $
��1060��6   $	��300��6-   .��5
   
   �  � ColumnDefinitions 5      �� 5      $��4* �6#   �� 5      $��1* �6#   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dgFelvetelizok��5         
IsReadOnly$
 True��6/   $��10q�6A   �� 5   
   $��1��6   
����� �6!   �� 5   
   $��1��6    �� Orientation$ Vertical=�6%   $��Center=�6<   
������ .+�5      �� -   5         ��Name$ 	btnImport��5      $��Importálás��6(   $	��175��6=   $��30��6I   $��15(�6U   $
��Bold"�6c   $��10q�6u   6�   �� 5      -   5        $ 	btnTorles��5      $��Törlés��6(   $	��175��69   $��30��6E   $��15(�6Q   $
��Bold"�6_   $��10q�6q   6}   �� 5      -   5        $ 	btnExport��5      $��Exportálás��6(   $	��175��6=   $��30��6I   $��15(�6U   $
��Bold"�6c   $��10q�6u   6�   �� 5      -   5        $ 
btnFelvesz��5      $��	Felvétel��6)   $	��175��6<   $��30��6H   $��15(�6T   $
��Bold"�6b   $��10q�6t   6�   �� 5      -   5        $ 
btnModosit��5      $��Módosítás��6)   $	��175��6=   $��30��6I   $��15(�6U   $
��Bold"�6c   $��10q�6u   6�   �� 5      -   5        $ 	btnBetolt��5      $��Betöltés adatbázisból��6(   $	��175��6H   $��30��6T   $��15(�6`   $
��Bold"�6n   $��10q�6�   6�   �� 5      -   5        $ 
btnFeltolt��5      $��Feltöltés adatbázisba��6)   $	��175��6I   $��30��6U   $��15(�6a   $
��Bold"�6o   $��10q�6�   6�   5      5      5      