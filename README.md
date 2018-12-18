
# Core.SimpleTemp
#我是抓取开始标记#
个人学习AspNetCore技术的Demo项目，地址：http://132.232.18.92:8080
<br>  
目前进行中：列表页更换为bootstrap-table , 
近 期 计划：认证/授权 采用Jwt，同时支持Web 以及客户端（App及其他）调用.

特点：自动生成查询表达式树,票据认证、精确的授权控制、自动DI、全局异常处理、分布式内存缓存、静态文件缓存、以及基于队列的文件日志、基于EFCore的支持多种数据库（目前演示Demo使用-PostgreSQL， 本地开发使用-Sql Server）


未来计划：
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

#我是抓取标记结束#