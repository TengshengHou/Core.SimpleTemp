# Core.SimpleTemp
Core.SimpleTemp
功能实现完全基于AspnetCore开发，个人学习AspNetcore 的Demo 项目

//Demo集成部署/环境
系统环境：Centos7.5 ; Cpu/1h ; 内存/1g  网络/1M （其实就是腾讯云10元/月）
数 据 库：PostgreSQL 9.5.15
持续集成：Jenkins  
进程守护：Supervisor


//前端
页面样式：bootstrap3
页面排版：AdminLte2
弹窗：Layer
select多选：select2
Tree：JsTree
分页插件:bootstrap-paginato

//后端
后端完全遵循AspNetCore标准开发
认证：基于CookieAuthentication
授权：自定义AuthorizationHandler+Filter
缓存：Distributed （目测服务器资源有限采用DistributedMemoryCache）
日志：采用基于队列的FileLog
数据访问：EFcore





