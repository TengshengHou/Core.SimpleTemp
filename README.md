# Core.SimpleTemp
<div class="myreadme">

Core.SimpleTemp
功能实现完全基于AspnetCore开发，个人学习AspNetcore 的Demo 项目 <br>
<br>
//Demo集成部署/环境<br>
系统环境：Centos7.5 ; Cpu/1h ; 内存/1g  网络/1M （其实就是腾讯云10元/月）<br>
数 据 库：PostgreSQL 9.5.15<br>
持续集成：Jenkins  <br>
进程守护：Supervisor<br>

<br>
//前端<br>
页面样式：bootstrap3<br>
页面排版：AdminLte2<br>
弹窗：Layer<br>
select多选：select2<br>
Tree：JsTree<br>
分页插件:bootstrap-paginato<br>
<br>
//后端<br>
后端完全遵循AspNetCore标准开发<br>
认证：基于CookieAuthentication<br>
授权：自定义AuthorizationHandler+Filter<br>
缓存：Distributed （目测服务器资源有限采用DistributedMemoryCache）<br>
日志：采用基于队列的FileLog<br>
数据访问：EFcore<br>





</div>