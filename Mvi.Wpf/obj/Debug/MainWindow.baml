   M S B A M L   `   `   `  ????B  >Mvi.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:Mvi.WpfMvi.Wpf  5   	       Mvi.Wpf.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Mvi.Wpf     5   	     ? Title$  
MainWindow??5	   	   $	??450??6   $	??800??6)    ? DataContext 5   	   ??  ?? RelativeSource ?? *Self+.??5
      ?  ? ColumnDefinitions 5   
   ?? 5      ?? 5       ? RowDefinitions 5   
   ?? 5      ?? 5      .+?5   
   ?? $??MeasureReuslt??6   $??5q?6*   .??5      ? .+?5      ?? .+?5      ?? $??PosNo :??6    ?? 5      $??TCS :??6    ?? 5      $??
Diameter :??6    ?? 5      $??Weight :??6    5      ?? 5       ? 	Resources 5       ?? 
TargetType ?? Property ?? Value??%?   '??       ?? 5      " ??6    .?5      ??  ? ??$ 50??66   ?? 5       ?@??TextAlignment$ Center=?6>   5       5!      .+?5"      ?? ??6    ?? *)'MviRs232_Controller.MeasureReuslt.PosNo+?? 5#      ??6    ?? *'%MviRs232_Controller.MeasureReuslt.TCS+?? 5$      ??6    ?? *,*MviRs232_Controller.MeasureReuslt.Diameter+?? 5%      ??6    ?? **(MviRs232_Controller.MeasureReuslt.Weight+5&      5'      5(      ?? 5)   
   $??LevelResult??6   $??1??6(   $??5q?68   .??5*      ? .+?5+      ?? .+?5,      ?? $	??D :??6    ?? 5-      $	??C :??6    ?? 5.      $
??Bs :??6    ?? 5/      $	??B :??6    ?? 50      $
??AN :??6    ?? 51      $
??AM :??6    52      ?? 53       54      	 ?? Triggers
 P? Property P? Value??%  '??       ?? 55      " ??6    .?56      ??  ? ??$ 50??66   ?? 57       ?@??TextAlignment$ Center=?6>   ?? 58       ? ??
 ?  ??6;   	 59      P? 5:   "   
 ? ??$
 True??6:   . ?5;   &   ??  ? ??
 ? ? ?6C   5<   #   5>      5?      .+?5@      ?? ??6    ?? *#!MviRs232_Controller.LevelResult.D+?? 5A      ??6    ?? *#!MviRs232_Controller.LevelResult.C+?? 5B      ??6    ?? *$"MviRs232_Controller.LevelResult.Bs+?? 5C      ??6    ?? *#!MviRs232_Controller.LevelResult.B+?? 5D      ??6    ?? *$"MviRs232_Controller.LevelResult.AN+?? 5E      ??6    ?? *$"MviRs232_Controller.LevelResult.AM+5F      5H      5I      ?? 5J   
   $??1??6   $??5q?6    $??ReceiveData Information??6+   .??5K      ?? .+?5L      ? .+?5M      ?? $??OK : ??6    ?? 5N      ??6    ?? *MviRs232_Controller.OKCount+5O      ? 5P      .+?5Q      ?? $??NG : ??6    ?? 5R      ??6    ?? *MviRs232_Controller.NGCount+5S      5T      5U      ?? 5V   
   -   5        $??1??5V      $??1??6!   $??Reset??6.   6>   5W      5X      