   M S B A M L   `   `   `  ����G  CSearchEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SearchEngineSearchEngine  5   	   !    SearchEngine.UserMainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  5   	   &localclr-namespace:SearchEngine  5   	     � 
ResizeMode$  CanMinimize=�5	   	   ���#D9D9D9 � Title$ Главное меню��5   	   $	��525��5   	   $
��1000��5   	    � Icon$ /Resources/Logo.ico��5   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � .+�5   
   �� $	��510��6   $��Center=�6"   .+�5      �� ���#373737$��75��5      $��0,0,0,0q�5      $��0q�5      ���#373737._�5      �� -   5        $��TitleTextBlock��5      $9��3Здравствуйте, Пользователь!��5      ���#D9D9D9$��10,10,10,10q�5      $��Center=�5      $��28(�5      5      �� 5       �� Orientation$ 
Horizontal=�6   $	��435��62   .+�5       �� $	��427��6   .+�5!      �� $ 
Horizontal=�6!   .+�5"      �� $5��/Текущий список 3D-сканеров��6$   $��10,10,10,10q�5#   $   $��24(�5$   $   $��Center=�5%   $   ���#373737$
��Left=�5'   $   �� 5(      -   5        $��RefreshDataGrid��5(   !   $��	225,0,0,0q�5)   !   $��🔄️��5*   !   $��18(�5+   !   ���#373737���#D9D9D9���#3737375/   !   �� 51      -   5        $��SearchButton��51   !   $��10,0,0,0q�52   !   $��Найти сканер��53   !   ���#373737���#D9D9D9���#373737$��Right=�57   !   58   !   59      �� 5:      $ 
Horizontal=�6!   $��0,0,0,0q�5;      $	��377��5<      .+�5=      &   System.Windows.Controls.DataGrid  -   5        $��ScannersTable��5?      ���#D9D9D9$��10,10,10,10q�5>      $	��750��5@      $	��Top=�5A         AutoGenerateColumns$ False��5B         CanUserAddRows$ False��5C         CanUserDeleteRows$ False��5D      $	��355��5E         Columns 5F      0  *System.Windows.Controls.DataGridTextColumn  5G   "   ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��65   
   
IsReadOnly$

 True��6A   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6S   �� *id+ �� UpdateSourceTrigger$ PropertyChanged=�  5H   "   $	 Название��65   $

 True��6G    6Y   �� *Name+$ PropertyChanged=�  5I   "   $ 	 Производитель��65   $

 True��6L    6^   �� *	Creator+$ PropertyChanged=�  5J   "   $	 Технология��65   $

 True��6I    6[   �� *
Technology+$ PropertyChanged=�  5K   "   $	 Точность (нм)��65   $

 True��6L    6^   �� *
Accuracy+$ PropertyChanged=�  5L   "   $&	  Скорость (точек/с)��65   $

 True��6Q    6c   �� *Speed+$ PropertyChanged=�  5M   "   $	 Захват цвета��65   $

 True��6K    6]   �� *ColorCapture+$ PropertyChanged=�  5N   "   $	 Цена (₽)��65   $

 True��6G    6Y   �� *Price+$ PropertyChanged=� �� StringFormat$ N2�� �� ConverterCulture$ ru-RU��  5O   "   $	 Год выпуска��65   $

 True��6J    6\   �� *	Release+$ PropertyChanged=�  5P   "   $	 Описание��65   $

 True��6G    6Y   �� *Description+$ PropertyChanged=�5R      5S      5T      �� 5U      "  WpfAnimatedGif.ImageBehavior   AnimatedSource$$ /Resources/MainWindowImage.gif��6      RepeatBehavior$ Forever��5V      $	��310��6D   5W      5X      5Y      5Z      