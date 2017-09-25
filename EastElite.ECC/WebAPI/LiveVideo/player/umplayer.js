"undefined" == typeof umplayer && (umplayer = function (d) {
	if (umplayer.api)
		return umplayer.api.selectPlayer(d)
}, 

umplayer.version = "1.0.1", 
umplayer.vid = document.createElement("video"), 
umplayer.audio = document.createElement("audio"), 
umplayer.source = document.createElement("source"), 

function (d) {
	function a(b) {
		return function () {
			return e(b)
		}
	}
	var h = document,
	g = window,
	c = navigator,
	b = d.utils = function () {};
	b.exists = function (b) {
		switch (typeof b) {
		case "string":
			return 0 < b.length;
		case "object":
			return null !== b;
		case "undefined":
			return !1
		}
		return !0
	};
	b.styleDimension = function (b) {
		return b + (0 < b.toString().indexOf("%") ? "" : "px")
	};
	b.getAbsolutePath = function (a, e) {
		b.exists(e) || (e = h.location.href);
		if (b.exists(a)) {
			var c;
			if (b.exists(a)) {
				c = a.indexOf("://");
				var g = a.indexOf("?");
				c = 0 < c && (0 > g || g > c)
			} else
				c = void 0;
			if (c)
				return a;
			c = e.substring(0, e.indexOf("://") + 3);
			var g = e.substring(c.length, e.indexOf("/", c.length + 1)),
			k;
			0 === a.indexOf("/") ? k = a.split("/") : (k = e.split("?")[0], k = k.substring(c.length + g.length + 1, k.lastIndexOf("/")), k = k.split("/").concat(a.split("/")));
			for (var f = [], d = 0; d < k.length; d++)
				k[d] && (b.exists(k[d]) && "." != k[d]) && (".." == k[d] ? f.pop() : f.push(k[d]));
			return c + g + "/" + f.join("/")
		}
	};
	b.extend = function () {
		var a = b.extend.arguments;
		if (1 < a.length) {
			for (var e = 1; e < a.length; e++)
				for (var c in a[e])
					try {
						b.exists(a[e][c]) && (a[0][c] = a[e][c])
					} catch (k) {}

			return a[0]
		}
		return null
	};
	b.log = function (b, a) {
		"undefined" != typeof console && "undefined" != typeof console.log && (a ? console.log(b, a) : console.log(b))
	};
	var e = b.userAgentMatch = function (b) {
		return null !== c.userAgent.toLowerCase().match(b)
	};
	b.isIE = a(/msie/i);
	b.isFF = a(/firefox/i);
	b.isChrome = a(/chrome/i);
	b.isIOS = a(/iP(hone|ad|od)/i);
	b.isIPod = a(/iP(hone|od)/i);
	b.isIPad = a(/iPad/i);
	b.isSafari602 = a(/Macintosh.*Mac OS X 10_8.*6\.0\.\d* Safari/i);
	b.isAndroid = function (b) {
		return b ? e(RegExp("android.*" + b, "i")) : e(/android/i)
	};
	b.isMobile = function () {
		return b.isIOS() || b.isAndroid()
	};
	b.saveCookie = function (b, a) {
		h.cookie = "umplayer." + b + "\x3d" + a + "; path\x3d/"
	};
	b.getCookies = function () {
		for (var b = {}, a = h.cookie.split("; "), e = 0; e < a.length; e++) {
			var c = a[e].split("\x3d");
			0 == c[0].indexOf("umplayer.") && (b[c[0].substring(9, c[0].length)] = c[1])
		}
		return b
	};
	b.typeOf = function (b) {
		var a = typeof b;
		return "object" === a ? !b ? "null" : b instanceof Array ? "array" : a : a
	};
	b.translateEventResponse = function (a, e) {
		var c = b.extend({}, e);
		a == d.events.UMPLAYER_FULLSCREEN && !c.fullscreen ? (c.fullscreen = "true" == c.message ? !0 : !1, delete c.message) : "object" == typeof c.data ? (c = b.extend(c, c.data), delete c.data) : "object" == typeof c.metadata && b.deepReplaceKeyName(c.metadata, ["__dot__", "__spc__", "__dsh__", "__default__"],
			[".", " ", "-", "default"]);
		var k = ["position", "duration", "offset"],
		g;
		for (g in k)
			c[k[g]] && (c[k[g]] = Math.round(1E3 * c[k[g]]) / 1E3);
		return c
	};
	b.flashVersion = function () {
		if (b.isAndroid())
			return 0;
		var a = c.plugins,
		e;
		try {
			if ("undefined" !== a && (e = a["Shockwave Flash"]))
				return parseInt(e.description.replace(/\D+(\d+)\..*/, "$1"))
		} catch (k) {}

		if ("undefined" != typeof g.ActiveXObject)
			try {
				if (e = new ActiveXObject("ShockwaveFlash.ShockwaveFlash"))
					return parseInt(e.GetVariable("$version").split(" ")[1].split(",")[0])
			} catch (d) {}

		return 0
	};
	b.getScriptPath = function (b) {
		for (var a = h.getElementsByTagName("script"), e = 0; e < a.length; e++) {
			var c = a[e].src;
			if (c && 0 <= c.indexOf(b))
				return c.substr(0, c.indexOf(b))
		}
		return ""
	};
	b.deepReplaceKeyName = function (b, a, e) {
		switch (d.utils.typeOf(b)) {
		case "array":
			for (var c = 0; c < b.length; c++)
				b[c] = d.utils.deepReplaceKeyName(b[c], a, e);
			break;
		case "object":
			for (var k in b) {
				var f;
				if (a instanceof Array && e instanceof Array)
					if (a.length != e.length)
						continue;
					else
						f = a;
				else
					f = [a];
				for (var g = k, c = 0; c < f.length; c++)
					g = g.replace(RegExp(a[c],
								"g"), e[c]);
				b[g] = d.utils.deepReplaceKeyName(b[k], a, e);
				k != g && delete b[k]
			}
		}
		return b
	};
	var k = b.pluginPathType = {
		ABSOLUTE : 0,
		RELATIVE : 1,
		CDN : 2
	};
	b.getPluginPathType = function (a) {
		if ("string" == typeof a) {
			a = a.split("?")[0];
			var e = a.indexOf("://");
			if (0 < e)
				return k.ABSOLUTE;
			var c = a.indexOf("/");
			a = b.extension(a);
			return 0 > e && 0 > c && (!a || !isNaN(a)) ? k.CDN : k.RELATIVE
		}
	};
	b.getPluginName = function (a) {
		return a.replace(/^(.*\/)?([^-]*)-?.*\.(swf|js)$/, "$2")
	};
	b.getPluginVersion = function (a) {
		return a.replace(/[^-]*-?([^\.]*).*$/, "$1")
	};
	b.isYouTube = function (a) {
		return -1 < a.indexOf("youtube.com") || -1 < a.indexOf("youtu.be")
	};
	b.isRtmp = function (a, b) {
		return 0 == a.indexOf("rtmp") || "rtmp" == b
	};
	b.foreach = function (a, b) {
		for (var e in a)
			a.hasOwnProperty(e) && b(e)
	};
	b.isHTTPS = function () {
		return 0 == g.location.href.indexOf("https")
	};
	b.repo = function () {
		var a = "./";//"http://p.umscdn.com/" + d.version.split(/\W/).splice(0, 2).join("/") + "/";
		try {
			b.isHTTPS() && (a = a.replace("http://", "https://ssl."))
		} catch (e) {}

		return a
	}
}(umplayer), 

function (d) {
	var a = "video/",
	h = {
		mp4 : a + "mp4",
		vorbis : "audio/ogg",
		ogg : a + "ogg",
		webm : a + "webm",
		aac : "audio/mp4",
		mp3 : "audio/mpeg",
		hls : "application/vnd.apple.mpegurl"
	},
	g = {
		mp4 : h.mp4,
		f4v : h.mp4,
		m4v : h.mp4,
		mov : h.mp4,
		m4a : h.aac,
		f4a : h.aac,
		aac : h.aac,
		mp3 : h.mp3,
		ogv : h.ogg,
		ogg : h.vorbis,
		oga : h.vorbis,
		webm : h.webm,
		m3u8 : h.hls,
		hls : h.hls
	},
	a = "video",
	a = {
		flv : a,
		f4v : a,
		mov : a,
		m4a : a,
		m4v : a,
		mp4 : a,
		aac : a,
		f4a : a,
		mp3 : "sound",
		smil : "rtmp",
		m3u8 : "hls",
		hls : "hls"
	},
	c = d.extensionmap = {},
	b;
	for (b in g)
		c[b] = {
			html5 : g[b]
		};
	for (b in a)
		c[b] || (c[b] = {}), c[b].flash = a[b];
	c.types = h;
	c.mimeType = function (a) {
		for (var b in h)
			if (h[b] ==
				a)
				return b
	};
	c.extType = function (a) {
		return c.mimeType(g[a])
	}
}(umplayer.utils), 

function (d) {
	var a = d.loaderstatus = {
		NEW : 0,
		LOADING : 1,
		ERROR : 2,
		COMPLETE : 3
	},
	h = document;
	d.scriptloader = function (g) {
		function c() {
			e = a.ERROR;
			l.sendEvent(k.ERROR)
		}
		function b() {
			e = a.COMPLETE;
			l.sendEvent(k.COMPLETE)
		}
		var e = a.NEW,
		k = umplayer.events,
		l = new k.eventdispatcher;
		d.extend(this, l);
		this.load = function () {
			var l = d.scriptloader.loaders[g];
			if (l && (l.getStatus() == a.NEW || l.getStatus() == a.LOADING))
				l.addEventListener(k.ERROR, c), l.addEventListener(k.COMPLETE,
					b);
			else if (d.scriptloader.loaders[g] = this, e == a.NEW) {
				e = a.LOADING;
				var n = h.createElement("script");
				n.addEventListener ? (n.onload = b, n.onerror = c) : n.readyState && (n.onreadystatechange = function () {
					("loaded" == n.readyState || "complete" == n.readyState) && b()
				});
				h.getElementsByTagName("head")[0].appendChild(n);
				n.src = g
			}
		};
		this.getStatus = function () {
			return e
		}
	};
	d.scriptloader.loaders = {}
}(umplayer.utils), 

function (d) {
	d.trim = function (a) {
		return a.replace(/^\s*/, "").replace(/\s*$/, "")
	};
	d.pad = function (a, d, g) {
		for (g || (g = "0"); a.length <
			d; )
			a = g + a;
		return a
	};
	d.xmlAttribute = function (a, d) {
		for (var g = 0; g < a.attributes.length; g++)
			if (a.attributes[g].name && a.attributes[g].name.toLowerCase() == d.toLowerCase())
				return a.attributes[g].value.toString();
		return ""
	};
	d.extension = function (a) {
		if (!a || "rtmp" == a.substr(0, 4))
			return "";
		a = a.substring(a.lastIndexOf("/") + 1, a.length).split("?")[0].split("#")[0];
		if (-1 < a.lastIndexOf("."))
			return a.substr(a.lastIndexOf(".") + 1, a.length).toLowerCase()
	};
	d.stringToColor = function (a) {
		a = a.replace(/(#|0x)?([0-9A-F]{3,6})$/gi,
				"$2");
		3 == a.length && (a = a.charAt(0) + a.charAt(0) + a.charAt(1) + a.charAt(1) + a.charAt(2) + a.charAt(2));
		return parseInt(a, 16)
	}
}(umplayer.utils), 

function (d) {
	d.key = function (a) {
		var h,
		g,
		c;
		this.edition = function () {
			return c && c.getTime() < (new Date).getTime() ? "invalid" : h
		};
		this.token = function () {
			return g
		};
		d.exists(a) || (a = "");
		try {
			a = d.tea.decrypt(a, "36QXq4W@GSBV^teR");
			var b = a.split("/");
			(h = b[0]) || (h = "free");
			g = b[1];
			b[2] && 0 < parseInt(b[2]) && (c = new Date, c.setTime(String(b[2])))
		} catch (e) {
			h = "invalid"
		}
	}
}(umplayer.utils), 

function (d) {
	var a =
		d.tea = {};
	a.encrypt = function (c, b) {
		if (0 == c.length)
			return "";
		var e = a.strToLongs(g.encode(c));
		1 >= e.length && (e[1] = 0);
		for (var k = a.strToLongs(g.encode(b).slice(0, 16)), l = e.length, d = e[l - 1], n = e[0], q, j = Math.floor(6 + 52 / l), f = 0; 0 < j--; ) {
			f += 2654435769;
			q = f >>> 2 & 3;
			for (var r = 0; r < l; r++)
				n = e[(r + 1) % l], d = (d >>> 5^n << 2) + (n >>> 3^d << 4)^(f^n) + (k[r & 3^q]^d), d = e[r] += d
		}
		e = a.longsToStr(e);
		return h.encode(e)
	};
	a.decrypt = function (c, b) {
		if (0 == c.length)
			return "";
		for (var e = a.strToLongs(h.decode(c)), k = a.strToLongs(g.encode(b).slice(0, 16)), d = e.length,
			m = e[d - 1], n = e[0], q, j = 2654435769 * Math.floor(6 + 52 / d); 0 != j; ) {
			q = j >>> 2 & 3;
			for (var f = d - 1; 0 <= f; f--)
				m = e[0 < f ? f - 1 : d - 1], m = (m >>> 5^n << 2) + (n >>> 3^m << 4)^(j^n) + (k[f & 3^q]^m), n = e[f] -= m;
			j -= 2654435769
		}
		e = a.longsToStr(e);
		e = e.replace(/\0+$/, "");
		return g.decode(e)
	};
	a.strToLongs = function (a) {
		for (var b = Array(Math.ceil(a.length / 4)), e = 0; e < b.length; e++)
			b[e] = a.charCodeAt(4 * e) + (a.charCodeAt(4 * e + 1) << 8) + (a.charCodeAt(4 * e + 2) << 16) + (a.charCodeAt(4 * e + 3) << 24);
		return b
	};
	a.longsToStr = function (a) {
		for (var b = Array(a.length), e = 0; e < a.length; e++)
			b[e] =
				String.fromCharCode(a[e] & 255, a[e] >>> 8 & 255, a[e] >>> 16 & 255, a[e] >>> 24 & 255);
		return b.join("")
	};
	var h = {
		code : "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/\x3d",
		encode : function (a, b) {
			var e,
			k,
			d,
			m,
			n = [],
			q = "",
			j,
			f,
			r = h.code;
			f = ("undefined" == typeof b ? 0 : b) ? g.encode(a) : a;
			j = f.length % 3;
			if (0 < j)
				for (; 3 > j++; )
					q += "\x3d", f += "\x00";
			for (j = 0; j < f.length; j += 3)
				e = f.charCodeAt(j), k = f.charCodeAt(j + 1), d = f.charCodeAt(j + 2), m = e << 16 | k << 8 | d, e = m >> 18 & 63, k = m >> 12 & 63, d = m >> 6 & 63, m &= 63, n[j / 3] = r.charAt(e) + r.charAt(k) + r.charAt(d) +
					r.charAt(m);
			n = n.join("");
			return n = n.slice(0, n.length - q.length) + q
		},
		decode : function (a, b) {
			b = "undefined" == typeof b ? !1 : b;
			var e,
			k,
			d,
			m,
			n,
			q = [],
			j,
			f = h.code;
			j = b ? g.decode(a) : a;
			for (var r = 0; r < j.length; r += 4)
				e = f.indexOf(j.charAt(r)), k = f.indexOf(j.charAt(r + 1)), m = f.indexOf(j.charAt(r + 2)), n = f.indexOf(j.charAt(r + 3)), d = e << 18 | k << 12 | m << 6 | n, e = d >>> 16 & 255, k = d >>> 8 & 255, d &= 255, q[r / 4] = String.fromCharCode(e, k, d), 64 == n && (q[r / 4] = String.fromCharCode(e, k)), 64 == m && (q[r / 4] = String.fromCharCode(e));
			m = q.join("");
			return b ? g.decode(m) : m
		}
	},
	g = {
		encode : function (a) {
			a = a.replace(/[\u0080-\u07ff]/g, function (a) {
					a = a.charCodeAt(0);
					return String.fromCharCode(192 | a >> 6, 128 | a & 63)
				});
			return a = a.replace(/[\u0800-\uffff]/g, function (a) {
					a = a.charCodeAt(0);
					return String.fromCharCode(224 | a >> 12, 128 | a >> 6 & 63, 128 | a & 63)
				})
		},
		decode : function (a) {
			a = a.replace(/[\u00e0-\u00ef][\u0080-\u00bf][\u0080-\u00bf]/g, function (a) {
					a = (a.charCodeAt(0) & 15) << 12 | (a.charCodeAt(1) & 63) << 6 | a.charCodeAt(2) & 63;
					return String.fromCharCode(a)
				});
			return a = a.replace(/[\u00c0-\u00df][\u0080-\u00bf]/g,
					function (a) {
					a = (a.charCodeAt(0) & 31) << 6 | a.charCodeAt(1) & 63;
					return String.fromCharCode(a)
				})
		}
	}
}(umplayer.utils), 

function (d) {
	d.events = {
		COMPLETE : "COMPLETE",
		ERROR : "ERROR",
		API_READY : "umplayerAPIReady",
		UMPLAYER_READY : "umplayerReady",
		UMPLAYER_FULLSCREEN : "umplayerFullscreen",
		UMPLAYER_RESIZE : "umplayerResize",
		UMPLAYER_ERROR : "umplayerError",
		UMPLAYER_MEDIA_BEFOREPLAY : "umplayerMediaBeforePlay",
		UMPLAYER_MEDIA_BEFORECOMPLETE : "umplayerMediaBeforeComplete",
		UMPLAYER_COMPONENT_SHOW : "umplayerComponentShow",
		UMPLAYER_COMPONENT_HIDE : "umplayerComponentHide",
		UMPLAYER_MEDIA_BUFFER : "umplayerMediaBuffer",
		UMPLAYER_MEDIA_BUFFER_FULL : "umplayerMediaBufferFull",
		UMPLAYER_MEDIA_ERROR : "umplayerMediaError",
		UMPLAYER_MEDIA_LOADED : "umplayerMediaLoaded",
		UMPLAYER_MEDIA_COMPLETE : "umplayerMediaComplete",
		UMPLAYER_MEDIA_SEEK : "umplayerMediaSeek",
		UMPLAYER_MEDIA_TIME : "umplayerMediaTime",
		UMPLAYER_MEDIA_VOLUME : "umplayerMediaVolume",
		UMPLAYER_MEDIA_META : "umplayerMediaMeta",
		UMPLAYER_MEDIA_MUTE : "umplayerMediaMute",
		UMPLAYER_MEDIA_LEVELS : "umplayerMediaLevels",
		UMPLAYER_MEDIA_LEVEL_CHANGED : "umplayerMediaLevelChanged",
		UMPLAYER_CAPTIONS_CHANGED : "umplayerCaptionsChanged",
		UMPLAYER_CAPTIONS_LIST : "umplayerCaptionsList",
		UMPLAYER_PLAYER_STATE : "umplayerState",
		state : {
			BUFFERING : "BUFFERING",
			IDLE : "IDLE",
			PAUSED : "PAUSED",
			PLAYING : "PLAYING"
		},
		UMPLAYER_PLAYLIST_LOADED : "umplayerPlaylistLoaded",
		UMPLAYER_PLAYLIST_ITEM : "umplayerPlaylistItem",
		UMPLAYER_PLAYLIST_COMPLETE : "umplayerPlaylistComplete",
		UMPLAYER_DISPLAY_CLICK : "umplayerViewClick",
		UMPLAYER_CONTROLS : "umplayerViewControls",
		UMPLAYER_INSTREAM_CLICK : "umplayerInstreamClicked",
		UMPLAYER_INSTREAM_DESTROYED : "umplayerInstreamDestroyed"
	}
}(umplayer), 

function (d) {
	var a = umplayer.utils;
	d.eventdispatcher = function (d, g) {
		var c,
		b;
		this.resetEventListeners = function () {
			c = {};
			b = []
		};
		this.resetEventListeners();
		this.addEventListener = function (b, k, d) {
			try {
				a.exists(c[b]) || (c[b] = []),
				"string" == a.typeOf(k) && (k = (new Function("return " + k))()),
				c[b].push({
					listener : k,
					count : d
				})
			} catch (g) {
				a.log("error", g)
			}
			return !1
		};
		this.removeEventListener = function (b, d) {
			if (c[b]) {
				try {
					for (var g = 0; g < c[b].length; g++)
						if (c[b][g].listener.toString() ==
							d.toString()) {
							c[b].splice(g, 1);
							break
						}
				} catch (h) {
					a.log("error", h)
				}
				return !1
			}
		};
		this.addGlobalListener = function (e, c) {
			try {
				"string" == a.typeOf(e) && (e = (new Function("return " + e))()),
				b.push({
					listener : e,
					count : c
				})
			} catch (d) {
				a.log("error", d)
			}
			return !1
		};
		this.removeGlobalListener = function (e) {
			if (e) {
				try {
					for (var c = 0; c < b.length; c++)
						if (b[c].listener.toString() == e.toString()) {
							b.splice(c, 1);
							break
						}
				} catch (d) {
					a.log("error", d)
				}
				return !1
			}
		};
		this.sendEvent = function (e, k) {
			a.exists(k) || (k = {});
			a.extend(k, {
				id : d,
				version : umplayer.version,
				type : e
			});
			g && a.log(e, k);
			if ("undefined" != a.typeOf(c[e]))
				for (var l = 0; l < c[e].length; l++) {
					try {
						c[e][l].listener(k)
					} catch (m) {
						a.log("There was an error while handling a listener: " + m.toString(), c[e][l].listener)
					}
					c[e][l] && (1 === c[e][l].count ? delete c[e][l] : 0 < c[e][l].count && (c[e][l].count -= 1))
				}
			for (l = 0; l < b.length; l++) {
				try {
					b[l].listener(k)
				} catch (n) {
					a.log("There was an error while handling a listener: " + n.toString(), b[l].listener)
				}
				b[l] && (1 === b[l].count ? delete b[l] : 0 < b[l].count && (b[l].count -= 1))
			}
		}
	}
}(umplayer.events),

function (d) {
	var a = {},
	h = {};
	d.plugins = function () {};
	d.plugins.loadPlugins = function (g, c) {
		h[g] = new d.plugins.pluginloader(new d.plugins.model(a), c);
		return h[g]
	};
	d.plugins.registerPlugin = function (g, c, b, e) {
		var k = d.utils.getPluginName(g);
		a[k] || (a[k] = new d.plugins.plugin(g));
		a[k].registerPlugin(g, c, b, e)
	}
}(umplayer), 

function (d) {
	d.plugins.model = function (a) {
		this.addPlugin = function (h) {
			var g = d.utils.getPluginName(h);
			a[g] || (a[g] = new d.plugins.plugin(h));
			return a[g]
		};
		this.getPlugins = function () {
			return a
		}
	}
}(umplayer),

function (d) {
	var a = umplayer.utils,
	h = umplayer.events;
	d.pluginmodes = {
		FLASH : 0,
		JAVASCRIPT : 1,
		HYBRID : 2
	};
	d.plugin = function (g) {
		function c() {
			switch (a.getPluginPathType(g)) {
			case a.pluginPathType.ABSOLUTE:
				return g;
			case a.pluginPathType.RELATIVE:
				return a.getAbsolutePath(g, window.location.href)
			}
		}
		function b() {
			q = setTimeout(function () {
					k = a.loaderstatus.COMPLETE;
					j.sendEvent(h.COMPLETE)
				}, 1E3)
		}
		function e() {
			k = a.loaderstatus.ERROR;
			j.sendEvent(h.ERROR)
		}
		var k = a.loaderstatus.NEW,
		l,
		m,
		n,
		q,
		j = new h.eventdispatcher;
		a.extend(this,
			j);
		this.load = function () {
			if (k == a.loaderstatus.NEW)
				if (0 < g.lastIndexOf(".swf"))
					l = g, k = a.loaderstatus.COMPLETE, j.sendEvent(h.COMPLETE);
				else if (a.getPluginPathType(g) == a.pluginPathType.CDN)
					k = a.loaderstatus.COMPLETE, j.sendEvent(h.COMPLETE);
				else {
					k = a.loaderstatus.LOADING;
					var f = new a.scriptloader(c());
					f.addEventListener(h.COMPLETE, b);
					f.addEventListener(h.ERROR, e);
					f.load()
				}
		};
		this.registerPlugin = function (b, e, c, d) {
			q && (clearTimeout(q), q = void 0);
			n = e;
			c && d ? (l = d, m = c) : "string" == typeof c ? l = c : "function" == typeof c ? m = c :
				!c && !d && (l = b);
			k = a.loaderstatus.COMPLETE;
			j.sendEvent(h.COMPLETE)
		};
		this.getStatus = function () {
			return k
		};
		this.getPluginName = function () {
			return a.getPluginName(g)
		};
		this.getFlashPath = function () {
			if (l)
				switch (a.getPluginPathType(l)) {
				case a.pluginPathType.ABSOLUTE:
					return l;
				case a.pluginPathType.RELATIVE:
					return 0 < g.lastIndexOf(".swf") ? a.getAbsolutePath(l, window.location.href) : a.getAbsolutePath(l, c())
				}
			return null
		};
		this.getJS = function () {
			return m
		};
		this.getTarget = function () {
			return n
		};
		this.getPluginmode = function () {
			if ("undefined" !=
				typeof l && "undefined" != typeof m)
				return d.pluginmodes.HYBRID;
			if ("undefined" != typeof l)
				return d.pluginmodes.FLASH;
			if ("undefined" != typeof m)
				return d.pluginmodes.JAVASCRIPT
		};
		this.getNewInstance = function (a, b, e) {
			return new m(a, b, e)
		};
		this.getURL = function () {
			return g
		}
	}
}(umplayer.plugins), 

function (d) {
	var a = d.utils,
	h = d.events;
	d.plugins.pluginloader = function (g, c) {
		function b() {
			m ? j.sendEvent(h.ERROR, {
				message : n
			}) : l || (l = !0, k = a.loaderstatus.COMPLETE, j.sendEvent(h.COMPLETE))
		}
		function e() {
			q || b();
			if (!l && !m) {
				var e = 0,
				c = g.getPlugins(),
				f;
				for (f in q) {
					var k = a.getPluginName(f),
					h = c[k],
					k = h.getJS(),
					j = h.getTarget(),
					h = h.getStatus();
					if (h == a.loaderstatus.LOADING || h == a.loaderstatus.NEW)
						e++;
					else if (k && (!j || parseFloat(j) > parseFloat(d.version)))
						m = !0, n = "Incompatible player version", b()
				}
				0 == e && b()
			}
		}
		var k = a.loaderstatus.NEW,
		l = !1,
		m = !1,
		n,
		q = c,
		j = new h.eventdispatcher;
		a.extend(this, j);
		this.setupPlugins = function (b, e, c) {
			var f = {
				length : 0,
				plugins : {}

			},
			d = 0,
			k = {},
			h = g.getPlugins(),
			l;
			for (l in e.plugins) {
				var j = a.getPluginName(l),
				m = h[j],
				B = m.getFlashPath(),
				n = m.getJS(),
				q = m.getURL();
				B && (f.plugins[B] = a.extend({}, e.plugins[l]), f.plugins[B].pluginmode = m.getPluginmode(), f.length++);
				try {
					if (n && e.plugins && e.plugins[q]) {
						var v = document.createElement("div");
						v.id = b.id + "_" + j;
						v.style.position = "absolute";
						v.style.top = 0;
						v.style.zIndex = d + 10;
						k[j] = m.getNewInstance(b, a.extend({}, e.plugins[q]), v);
						d++;
						b.onReady(c(k[j], v, !0));
						b.onResize(c(k[j], v))
					}
				} catch (C) {
					a.log("ERROR: Failed to load " + j + ".")
				}
			}
			b.plugins = k;
			return f
		};
		this.load = function () {
			if (!(a.exists(c) && "object" != a.typeOf(c))) {
				k =
					a.loaderstatus.LOADING;
				for (var b in c)
					if (a.exists(b)) {
						var d = g.addPlugin(b);
						d.addEventListener(h.COMPLETE, e);
						d.addEventListener(h.ERROR, f)
					}
				d = g.getPlugins();
				for (b in d)
					d[b].load()
			}
			e()
		};
		var f = this.pluginFailed = function () {
			m || (m = !0, n = "File not found", b())
		};
		this.getStatus = function () {
			return k
		}
	}
}(umplayer), 

function (d) {
	d.playlist = function (a) {
		var h = [];
		if ("array" == d.utils.typeOf(a))
			for (var g = 0; g < a.length; g++)
				h.push(new d.playlist.item(a[g]));
		else
			h.push(new d.playlist.item(a));
		return h
	}
}(umplayer), 

/*
function (d) {
	var a =
		d.item = function (h) {
		var g = umplayer.utils,
		c = g.extend({}, a.defaults, h);
		c.tracks = g.exists(h.tracks) ? h.tracks : [];
		0 == c.sources.length && (c.sources = [new d.source(c)]);
		for (var b = 0; b < c.sources.length; b++) {
			var e = c.sources[b]["default"];
			c.sources[b]["default"] = e ? "true" == e.toString() : !1;
			c.sources[b] = new d.source(c.sources[b])
		}
		if (c.captions && !g.exists(h.tracks)) {
			for (h = 0; h < c.captions.length; h++)
				c.tracks.push(c.captions[h]);
			delete c.captions
		}
		for (b = 0; b < c.tracks.length; b++)
			c.tracks[b] = new d.track(c.tracks[b]);
		return c
	};
	a.defaults = {
		description : "",
		image : "",
		mediaid : "",
		title : "",
		sources : [],
		tracks : []
	}
}(umplayer.playlist), 
*/

(function(playlist) {
	var _item = playlist.item = function(config) {
		var utils = umplayer.utils,
			_playlistitem = utils.extend({}, _item.defaults, config);
		_playlistitem.tracks = utils.exists(config.tracks) ? config.tracks : [];

		if (_playlistitem.sources.length == 0) {
			_playlistitem.sources = [new playlist.source(_playlistitem)];
		}

		/** Each source should be a named object **/
		for (var i=0; i < _playlistitem.sources.length; i++) {
			var def = _playlistitem.sources[i]["default"];
			if (def) {
				_playlistitem.sources[i]["default"] = (def.toString() == "true");
			}
			else {
				_playlistitem.sources[i]["default"] = false;	
			}

			_playlistitem.sources[i] = new playlist.source(_playlistitem.sources[i]);
		}

		if (_playlistitem.captions && !utils.exists(config.tracks)) {
			for (var j = 0; j < _playlistitem.captions.length; j++) {
				_playlistitem.tracks.push(_playlistitem.captions[j]);
			}
			delete _playlistitem.captions;
		}

		for (var i=0; i < _playlistitem.tracks.length; i++) {
			_playlistitem.tracks[i] = new playlist.track(_playlistitem.tracks[i]);
		}
		return _playlistitem;
	};
	
	_item.defaults = {
		description: "",
		image: "",
		mediaid: "",
		title: "",
		sources: [],
		tracks: []
	};
	
})(umplayer.playlist),

function (d) {
	var a = umplayer.utils,
	h = {
		file : void 0,
		label : void 0,
		type : void 0,
		"default" : void 0
	};
	d.source = function (d) {
		var c = a.extend({}, h),
		b;
		for (b in h)
			a.exists(d[b]) && (c[b] = d[b], delete d[b]);
		c.type && 0 < c.type.indexOf("/") && (c.type = a.extensionmap.mimeType(c.type));
		"m3u8" == c.type && (c.type = "hls");
		"smil" == c.type && (c.type = "rtmp");
		return c
	}
}(umplayer.playlist), 

function (d) {
	var a = umplayer.utils,
	h = {
		file : void 0,
		label : void 0,
		kind : "captions",
		"default" : !1
	};
	d.track = function (d) {
		var c = a.extend({}, h);
		d || (d = {});
		for (var b in h)
			a.exists(d[b]) && (c[b] = d[b], delete d[b]);
		return c
	}
}(umplayer.playlist), 

function (d) {
	var a = d.utils,
	h = d.events,
	g = document,
	c = d.embed = function (b) {
		function e(a) {
			l(n, p + a.message)
		}
		function k() {
			l(n, p + "No playable sources found")
		}
		function l(b, e) {
			if (m.fallback) {
				var c = b.style;
				c.backgroundColor = "#000";
				c.color = "#FFF";
				c.width = a.styleDimension(m.width);
				c.height = a.styleDimension(m.height);
				c.display = "table";
				c.opacity = 1;
				var c =
					document.createElement("p"),
				d = c.style;
				d.verticalAlign = "middle";
				d.textAlign = "center";
				d.display = "table-cell";
				d.font = "15px/20px Arial, Helvetica, sans-serif";
				c.innerHTML = e.replace(":", ":\x3cbr\x3e");
				b.innerHTML = "";
				b.appendChild(c)
			}
		}
		var m = new c.config(b.config),
		n,
		q,
		j,
		f = m.width,
		r = m.height,
		p = "Error loading player: ",
		s = d.plugins.loadPlugins(b.id, m.plugins);
		m.fallbackDiv && (j = m.fallbackDiv, delete m.fallbackDiv);
		m.id = b.id;
		q = g.getElementById(b.id);
		n = g.createElement("div");
		n.id = q.id;
		n.style.width = 0 < f.toString().indexOf("%") ?
			f : f + "px";
		n.style.height = 0 < r.toString().indexOf("%") ? r : r + "px";
		q.parentNode.replaceChild(n, q);
		d.embed.errorScreen = l;
		s.addEventListener(h.COMPLETE, function () {
			if ("array" == a.typeOf(m.playlist) && 2 > m.playlist.length && (0 == m.playlist.length || !m.playlist[0].sources || 0 == m.playlist[0].sources.length)){
				k();
			}
			else if (s.getStatus() == a.loaderstatus.COMPLETE) {
				for (var d = 0; d < m.modes.length; d++)
					if (m.modes[d].type && c[m.modes[d].type]) {
						var f = a.extend({}, m),
						g = new c[m.modes[d].type](n, m.modes[d], f, s, b);
						if (g.supportsConfig()) {
							g.addEventListener(h.ERROR,
								e);
							g.embed();
							d = b;
							f = f.events;
							g = void 0;
							for (g in f)
								"function" == typeof d[g] && d[g].call(d, f[g]);
							return b
						}
					}
				m.fallback ? (a.log("No suitable players found and fallback enabled"), new c.download(n, m, k)) : (a.log("No suitable players found and fallback disabled"), n.parentNode.replaceChild(j, n))
			}
		});
		s.addEventListener(h.ERROR, function (a) {
			l(n, "Could not load plugins: " + a.message)
		});
		s.load();
		return b
	}
}(umplayer), 

function (d) {
	function a(a) {
		if (a.playlist)
			for (var e = 0; e < a.playlist.length; e++)
				a.playlist[e] = new c(a.playlist[e]);
		else {
			var e = {},
			d;
			for (d in c.defaults)
				h(a, e, d);
			e.sources || (a.levels ? (e.sources = a.levels, delete a.levels) : (d = {}, h(a, d, "file"), h(a, d, "type"), e.sources = d.file ? [d] : []));
			a.playlist = [new c(e)]
		}
	}
	function h(a, e, c) {
		g.exists(a[c]) && (e[c] = a[c], delete a[c])
	}
	var g = d.utils,
	c = d.playlist.item;
	(d.embed.config = function (b) {
		var e = {
			fallback : !0,
			height : 270,
			primary : "html5",
			width : 480,
			base : b.base ? b.base : g.getScriptPath("umplayer.js")
		};
		b = g.extend(e, d.defaults, b);
		var e = {
			type : "html5",
			src : b.base + "umplayer.html5.js"
		},
		c = {
			type : "flash",
			src : b.base + "umplayer.flash.swf"
		};
		b.modes = "flash" == b.primary ? [c, e] : [e, c];
		b.listbar && (b.playlistsize = b.listbar.size, b.playlistposition = b.listbar.position);
		b.flashplayer && (c.src = b.flashplayer);
		b.html5player && (e.src = b.html5player);
		a(b);
		return b
	}).addConfig = function (b, c) {
		a(c);
		return g.extend(b, c)
	}
}(umplayer), 

function (d) {
	var a = d.utils,
	h = document;
	d.embed.download = function (d, c, b) {
		function e(a, b) {
			for (var c = h.querySelectorAll(a), e = 0; e < c.length; e++)
				for (var d in b)
					c[e].style[d] = b[d]
		}
		function k(a, b, c) {
			a = h.createElement(a);
			b && (a.className = "umdownload" + b);
			c && c.appendChild(a);
			return a
		}
		var l = a.extend({}, c),
		m,
		n = l.width ? l.width : 480,
		q = l.height ? l.height : 320,
		j;
		c = c.logo ? c.logo : {
			prefix : a.repo(),
			file : "logo.png",
			margin : 10
		};
		var f,
		r;
		r = l.playlist;
		var p,
		s,
		l = ["mp4", "aac", "mp3"];
		if (r && r.length) {
			p = r[0];
			s = p.sources;
			for (r = 0; r < s.length; r++) {
				var u = s[r],
				t = u.type ? u.type : a.extensionmap.extType(a.extension(u.file));
				if (u.file)
					for (r in l)
						t == l[r] ? (m = u.file, j = p.image) : a.isYouTube(u.file) && (f = u.file)
			}
			m ? (b = m, d && (m = k("a", "display", d), k("div", "icon",
						m), k("div", "logo", m), b && m.setAttribute("href", a.getAbsolutePath(b))), b = "#" + d.id + " .umdownload", d.style.width = "", d.style.height = "", e(b + "display", {
					width : a.styleDimension(Math.max(320, n)),
					height : a.styleDimension(Math.max(180, q)),
					background : "black center no-repeat " + (j ? "url(" + j + ")" : ""),
					backgroundSize : "contain",
					position : "relative",
					border : "none",
					display : "block"
				}), e(b + "display div", {
					position : "absolute",
					width : "100%",
					height : "100%"
				}), e(b + "logo", {
					top : c.margin + "px",
					right : c.margin + "px",
					background : "top right no-repeat url(" +
					c.prefix + c.file + ")"
				}), e(b + "icon", {
					background : "center no-repeat url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADwAAAA8CAYAAAA6/NlyAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAgNJREFUeNrs28lqwkAYB/CZqNVDDj2r6FN41QeIy8Fe+gj6BL275Q08u9FbT8ZdwVfotSBYEPUkxFOoks4EKiJdaDuTjMn3wWBO0V/+sySR8SNSqVRKIR8qaXHkzlqS9jCfzzWcTCYp9hF5o+59sVjsiRzcegSckFzcjT+ruN80TeSlAjCAAXzdJSGPFXRpAAMYwACGZQkSdhG4WCzehMNhqV6vG6vVSrirKVEw66YoSqDb7cqlUilE8JjHd/y1MQefVzqdDmiaJpfLZWHgXMHn8F6vJ1cqlVAkEsGuAn83J4gAd2RZymQygX6/L1erVQt+9ZPWb+CDwcCC2zXGJaewl/DhcHhK3DVj+KfKZrMWvFarcYNLomAv4aPRSFZVlTlcSPA5fDweW/BoNIqFnKV53JvncjkLns/n/cLdS+92O7RYLLgsKfv9/t8XlDn4eDyiw+HA9Jyz2eyt0+kY2+3WFC5hluej0Ha7zQQq9PPwdDq1Et1sNsx/nFBgCqWJ8oAK1aUptNVqcYWewE4nahfU0YQnk4ntUEfGMIU2m01HoLaCKbTRaDgKtaVLk9tBYaBcE/6Artdr4RZ5TB6/dC+9iIe/WgAMYADDpAUJAxjAAAYwgGFZgoS/AtNNTF7Z2bL0BYPBV3Jw5xFwwWcYxgtBP5OkE8i9G7aWGOOCruvauwADALMLMEbKf4SdAAAAAElFTkSuQmCC)"
				})) :
			f ? (c = f, d = k("embed", "", d), d.src = "http://www.youtube.com/v/" + /v[=\/](\w*)|\/(\w+)$|^(\w+)$/i.exec(c).slice(1).join(""), d.type = "application/x-shockwave-flash", d.width = n, d.height = q) : b()
		}
	}
}(umplayer), 

function (d) {
	var a = d.utils,
	h = d.events,
	g = {};
	(d.embed.flash = function (c, b, e, k, l) {
		function m(a, b, c) {
			var e = document.createElement("param");
			e.setAttribute("name", b);
			e.setAttribute("value", c);
			a.appendChild(e)
		}
		function n(a, b, c) {
			return function () {
				try {
					c && document.getElementById(l.id + "_wrapper").appendChild(b);
					var e =
						document.getElementById(l.id).getPluginConfig("display");
					"function" == typeof a.resize && a.resize(e.width, e.height);
					b.style.left = e.x;
					b.style.top = e.h
				} catch (d) {}

			}
		}
		function q(b) {
			if (!b)
				return {};
			var c = {},
			e = [],
			d;
			for (d in b) {
				var f = a.getPluginName(d),
				g = b[d];
				e.push(d);
				for (var k in g)
					c[f + "." + k] = g[k]
			}
			c.plugins = e.join(",");
			return c
		}
		var j = new d.events.eventdispatcher,
		f = a.flashVersion();
		a.extend(this, j);
		this.embed = function () {
			e.id = l.id;
			if (10 > f)
				return j.sendEvent(h.ERROR, {
					message : "Flash version must be 10.0 or greater"
				}),
				!1;
			var d,
			p = a.extend({}, e);
			c.id + "_wrapper" == c.parentNode.id ? document.getElementById(c.id + "_wrapper") : (d = document.createElement("div"), d.id = c.id + "_wrapper", d.style.position = "relative", d.style.width = a.styleDimension(p.width), d.style.height = a.styleDimension(p.height), c.parentNode.replaceChild(d, c), d.appendChild(c));
			d = k.setupPlugins(l, p, n);
			0 < d.length ? a.extend(p, q(d.plugins)) : delete p.plugins;
			"undefined" != typeof p["dock.position"] && "false" == p["dock.position"].toString().toLowerCase() && (p.dock = p["dock.position"],
				delete p["dock.position"]);
			d = p.wmode ? p.wmode : p.height && 40 >= p.height ? "transparent" : "opaque";
			for (var s = "height width modes events primary base fallback volume".split(" "), u = 0; u < s.length; u++)
				delete p[s[u]];
			var s = a.getCookies(),
			t;
			for (t in s)
				"undefined" == typeof p[t] && (p[t] = s[t]);
			t = window.location.pathname.split("/");
			t.splice(t.length - 1, 1);
			t = t.join("/");
			p.base = t + "/";
			g[c.id] = p;
			a.isIE() ? (p = '\x3cobject classid\x3d"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" " width\x3d"100%" height\x3d"100%" id\x3d"' + c.id +
					'" name\x3d"' + c.id + '" tabindex\x3d0""\x3e', p += '\x3cparam name\x3d"movie" value\x3d"' + b.src + '"\x3e', p += '\x3cparam name\x3d"allowfullscreen" value\x3d"true"\x3e\x3cparam name\x3d"allowscriptaccess" value\x3d"always"\x3e', p += '\x3cparam name\x3d"seamlesstabbing" value\x3d"true"\x3e', p += '\x3cparam name\x3d"wmode" value\x3d"' + d + '"\x3e', p += '\x3cparam name\x3d"bgcolor" value\x3d"#000000"\x3e', p += "\x3c/object\x3e", c.outerHTML = p, p = document.getElementById(c.id)) : (p = document.createElement("object"), p.setAttribute("type",
					"application/x-shockwave-flash"), p.setAttribute("data", b.src), p.setAttribute("width", "100%"), p.setAttribute("height", "100%"), p.setAttribute("bgcolor", "#000000"), p.setAttribute("id", c.id), p.setAttribute("name", c.id), p.setAttribute("tabindex", 0), m(p, "allowfullscreen", "true"), m(p, "allowscriptaccess", "always"), m(p, "seamlesstabbing", "true"), m(p, "wmode", d), c.parentNode.replaceChild(p, c));
			l.container = p;
			l.setPlayer(p, "flash")
		};
		this.supportsConfig = function () {
			if (f)
				if (e) {
					if ("string" == a.typeOf(e.playlist))
						return !0;
					try {
						var b = e.playlist[0].sources;
						if ("undefined" == typeof b)
							return !0;
						for (var c = 0; c < b.length; c++) {
							var d;
							if (d = b[c].file) {
								var g = b[c].file,
								k = b[c].type;
								if (a.isYouTube(g) || a.isRtmp(g, k) || "hls" == k)
									d = !0;
								else {
									var h = a.extensionmap[k ? k : a.extension(g)];
									d = !h ? !1 : !!h.flash
								}
							}
							if (d)
								return !0
						}
					} catch (j) {}

				} else
					return !0;
			return !1
		}
	}).getVars = function (a) {
		return g[a]
	}
}(umplayer), 

function (d) {
	var a = d.utils,
	h = a.extensionmap,
	g = d.events;
	d.embed.html5 = function (c, b, e, k, l) {
		function m(a, b, d) {
			return function () {
				try {
					var e = document.querySelector("#" +
							c.id + " .ummain");
					d && e.appendChild(b);
					"function" == typeof a.resize && (a.resize(e.clientWidth, e.clientHeight), setTimeout(function () {
							a.resize(e.clientWidth, e.clientHeight)
						}, 400));
					b.left = e.style.left;
					b.top = e.style.top
				} catch (g) {}

			}
		}
		function n(a) {
			q.sendEvent(a.type, {
				message : "HTML5 player not found"
			})
		}
		var q = this,
		j = new g.eventdispatcher;
		a.extend(q, j);
		q.embed = function () {
			if (d.html5) {
				k.setupPlugins(l, e, m);
				c.innerHTML = "";
				var f = d.utils.extend({}, e);
				delete f.volume;
				f = new d.html5.player(f);
				l.container = document.getElementById(l.id);
				l.setPlayer(f, "html5")
			} else
				f = new a.scriptloader(b.src), f.addEventListener(g.ERROR, n), f.addEventListener(g.COMPLETE, q.embed), f.load()
		};
		q.supportsConfig = function () {
			if (d.vid.canPlayType)
				try {
					if ("string" == a.typeOf(e.playlist))
						return !0;
					for (var b = e.playlist[0].sources, c = 0; c < b.length; c++) {
						var g;
						var k = b[c].file,
						j = b[c].type;
						if (null !== navigator.userAgent.match(/BlackBerry/i) || a.isAndroid() && ("m3u" == a.extension(k) || "m3u8" == a.extension(k)) || a.isRtmp(k, j))
							g = !1;
						else {
							var l = h[j ? j : a.extension(k)],
							m;
							if (!l || l.flash &&
								!l.html5)
								m = !1;
							else {
								var n = l.html5,
								q = d.vid;
								if (n)
									try {
										m = q.canPlayType(n) ? !0 : !1
									} catch (z) {
										m = !1
									}
								else
									m = !0
							}
							g = m
						}
						if (g)
							return !0
					}
				} catch (A) {}

			return !1
		}
	}
}(umplayer), 

function (d) {
	var a = d.embed,
	h = d.utils,
	g = h.extend(function (c) {/*
			var b = h.repo(),
			e = c.config,
			g = e.plugins,
			l = e.analytics,
			m = b + "umpsrv.js",
			n = b + "sharing.js",
			q = b + "related.js",
			j = b + "gapro.js",
			f = "premium",//(new d.utils.key(d.key)).edition(),
			g = g ? g : {};
			"ads" == f && e.advertising && (e.advertising.client.match(".js$|.swf$") ? g[e.advertising.client] = e.advertising : g[b + e.advertising.client +
						("flash" == e.primary ? ".swf" : ".js")] = e.advertising);
			delete e.advertising;
			e.key = d.key;
			e.analytics && (e.analytics.client && e.analytics.client.match(".js$|.swf$")) && (m = e.analytics.client);
			delete e.analytics;
			if ("free" == f || !l || !1 !== l.enabled)
				g[m] = l ? l : {};
			delete g.sharing;
			delete g.related;
			if ("premium" == f || "ads" == f)
				e.sharing && (e.sharing.client && e.sharing.client.match(".js$|.swf$") && (n = e.sharing.client), g[n] = e.sharing), e.related && (e.related.client && e.related.client.match(".js$|.swf$") && (q = e.related.client), g[q] =
						e.related), e.ga && (e.ga.client && e.ga.client.match(".js$|.swf$") && (j = e.ga.client), g[j] = e.ga), e.skin && (e.skin = e.skin.replace(/^(beelden|bekle|five|glow|modieus|roundster|stormtrooper|vapor)$/i, h.repo() + "skins/$1.xml").toLowerCase());
			e.plugins = g;*/
			return new a(c)
		}, a);
	d.embed = g
}(umplayer), 

function (d) {
	var a = [],
	h = d.utils,
	g = d.events,
	c = g.state,
	b = document,
	e = d.api = function (a) {
		function l(a, b) {
			return function (c) {
				return b(a, c)
			}
		}
		function m(a, b) {
			p[a] || (p[a] = [], q(g.UMPLAYER_PLAYER_STATE, function (b) {
					var c = b.newstate;
					b = b.oldstate;
					if (c == a) {
						var e = p[c];
						if (e)
							for (var d = 0; d < e.length; d++)
								"function" == typeof e[d] && e[d].call(this, {
									oldstate : b,
									newstate : c
								})
					}
				}));
			p[a].push(b);
			return f
		}
		function n(a, b) {
			try {
				a.umAddEventListener(b, 'function(dat) { umplayer("' + f.id + '").dispatchEvent("' + b + '", dat); }')
			} catch (c) {
				h.log("Could not add internal listener")
			}
		}
		function q(a, b) {
			r[a] || (r[a] = [], s && u && n(s, a));
			r[a].push(b);
			return f
		}
		function j() {
			if (u) {
				for (var a = arguments[0], b = [], c = 1; c < arguments.length; c++)
					b.push(arguments[c]);
				if ("undefined" != typeof s &&
					"function" == typeof s[a])
					switch (b.length) {
					case 4:
						return s[a](b[0], b[1], b[2], b[3]);
					case 3:
						return s[a](b[0], b[1], b[2]);
					case 2:
						return s[a](b[0], b[1]);
					case 1:
						return s[a](b[0]);
					default:
						return s[a]()
					}
				return null
			}
			t.push(arguments)
		}
		var f = this,
		r = {},
		p = {},
		s = void 0,
		u = !1,
		t = [],
		w = void 0,
		x = {},
		y = {};
		f.container = a;
		f.id = a.id;
		f.getBuffer = function () {
			return j("umGetBuffer")
		};
		f.getContainer = function () {
			return f.container
		};
		f.addButton = function (a, b, c, e) {
			try {
				y[e] = c,
				j("umDockAddButton", a, b, "umplayer('" + f.id + "').callback('" + e +
					"')", e)
			} catch (d) {
				h.log("Could not add dock button" + d.message)
			}
		};
		f.removeButton = function (a) {
			j("umDockRemoveButton", a)
		};
		f.callback = function (a) {
			if (y[a])
				y[a]()
		};
		f.forceState = function (a) {
			j("umForceState", a);
			return f
		};
		f.releaseState = function () {
			return j("umReleaseState")
		};
		f.getDuration = function () {
			return j("umGetDuration")
		};
		f.getFullscreen = function () {
			return j("umGetFullscreen")
		};
		f.getStretching = function () {
			return j("umGetStretching")
		};
		f.getHeight = function () {
			return j("umGetHeight")
		};
		f.getLockState = function () {
			return j("umGetLockState")
		};
		f.getMeta = function () {
			return f.getItemMeta()
		};
		f.getMute = function () {
			return j("umGetMute")
		};
		f.getPlaylist = function () {
			var a = j("umGetPlaylist");
			"flash" == f.renderingMode && h.deepReplaceKeyName(a, ["__dot__", "__spc__", "__dsh__", "__default__"], [".", " ", "-", "default"]);
			return a
		};
		f.getPlaylistItem = function (a) {
			h.exists(a) || (a = f.getCurrentItem());
			return f.getPlaylist()[a]
		};
		f.getPosition = function () {
			return j("umGetPosition")
		};
		f.getRenderingMode = function () {
			return f.renderingMode
		};
		f.getState = function () {
			return j("umGetState")
		};
		f.getVolume = function () {
			return j("umGetVolume")
		};
		f.getWidth = function () {
			return j("umGetWidth")
		};
		f.setFullscreen = function (a) {
			h.exists(a) ? j("umSetFullscreen", a) : j("umSetFullscreen", !j("umGetFullscreen"));
			return f
		};
		f.setStretching = function (a) {
			j("umSetStretching", a);
			return f
		};
		f.setMute = function (a) {
			h.exists(a) ? j("umSetMute", a) : j("umSetMute", !j("umGetMute"));
			return f
		};
		f.lock = function () {
			return f
		};
		f.unlock = function () {
			return f
		};
		f.load = function (a) {
			j("umLoad", a);
			return f
		};
		f.playlistItem = function (a) {
			j("umPlaylistItem",
				parseInt(a));
			return f
		};
		f.playlistPrev = function () {
			j("umPlaylistPrev");
			return f
		};
		f.playlistNext = function () {
			j("umPlaylistNext");
			return f
		};
		f.resize = function (a, c) {
			if ("flash" != f.renderingMode)
				j("umResize", a, c);
			else {
				var e = b.getElementById(f.id + "_wrapper");
				e && (e.style.width = h.styleDimension(a), e.style.height = h.styleDimension(c))
			}
			return f
		};
		f.play = function (a) {
			"undefined" == typeof a ? (a = f.getState(), a == c.PLAYING || a == c.BUFFERING ? j("umPause") : j("umPlay")) : j("umPlay", a);
			return f
		};
		f.pause = function (a) {
			"undefined" ==
			typeof a ? (a = f.getState(), a == c.PLAYING || a == c.BUFFERING ? j("umPause") : j("umPlay")) : j("umPause", a);
			return f
		};
		f.stop = function () {
			j("umStop");
			return f
		};
		f.seek = function (a) {
			j("umSeek", a);
			return f
		};
		f.setVolume = function (a) {
			j("umSetVolume", a);
			return f
		};
		f.loadInstream = function (a, b) {
			return w = new e.instream(this, s, a, b)
		};
		f.getQualityLevels = function () {
			return j("umGetQualityLevels")
		};
		f.getCurrentQuality = function () {
			return j("umGetCurrentQuality")
		};
		f.setCurrentQuality = function (a) {
			j("umSetCurrentQuality", a)
		};
		f.getCaptionsList =
		function () {
			return j("umGetCaptionsList")
		};
		f.getCurrentCaptions = function () {
			return j("umGetCurrentCaptions")
		};
		f.setCurrentCaptions = function (a) {
			j("umSetCurrentCaptions", a)
		};
		f.getControls = function () {
			return j("umGetControls")
		};
		f.getSafeRegion = function () {
			return j("umGetSafeRegion")
		};
		f.setControls = function (a) {
			j("umSetControls", a)
		};
		f.destroyPlayer = function () {
			j("umPlayerDestroy")
		};
		var z = {
			onBufferChange : g.UMPLAYER_MEDIA_BUFFER,
			onBufferFull : g.UMPLAYER_MEDIA_BUFFER_FULL,
			onError : g.UMPLAYER_ERROR,
			onFullscreen : g.UMPLAYER_FULLSCREEN,
			onMeta : g.UMPLAYER_MEDIA_META,
			onMute : g.UMPLAYER_MEDIA_MUTE,
			onPlaylist : g.UMPLAYER_PLAYLIST_LOADED,
			onPlaylistItem : g.UMPLAYER_PLAYLIST_ITEM,
			onPlaylistComplete : g.UMPLAYER_PLAYLIST_COMPLETE,
			onReady : g.API_READY,
			onResize : g.UMPLAYER_RESIZE,
			onComplete : g.UMPLAYER_MEDIA_COMPLETE,
			onSeek : g.UMPLAYER_MEDIA_SEEK,
			onTime : g.UMPLAYER_MEDIA_TIME,
			onVolume : g.UMPLAYER_MEDIA_VOLUME,
			onBeforePlay : g.UMPLAYER_MEDIA_BEFOREPLAY,
			onBeforeComplete : g.UMPLAYER_MEDIA_BEFORECOMPLETE,
			onDisplayClick : g.UMPLAYER_DISPLAY_CLICK,
			onControls : g.UMPLAYER_CONTROLS,
			onQualityLevels : g.UMPLAYER_MEDIA_LEVELS,
			onQualityChange : g.UMPLAYER_MEDIA_LEVEL_CHANGED,
			onCaptionsList : g.UMPLAYER_CAPTIONS_LIST,
			onCaptionsChange : g.UMPLAYER_CAPTIONS_CHANGED
		};
		h.foreach(z, function (a) {
			f[a] = l(z[a], q)
		});
		var A = {
			onBuffer : c.BUFFERING,
			onPause : c.PAUSED,
			onPlay : c.PLAYING,
			onIdle : c.IDLE
		};
		h.foreach(A, function (a) {
			f[a] = l(A[a], m)
		});
		f.remove = function () {
			if (!u)
				throw "Cannot call remove() before player is ready";
			t = [];
			e.destroyPlayer(this.id)
		};
		f.setup = function (a) {
			if (d.embed) {
				var c = b.getElementById(f.id);
				c && (a.fallbackDiv = c);
				c = f;
				t = [];
				e.destroyPlayer(c.id);
				c = d(f.id);
				c.config = a;
				return new d.embed(c)
			}
			return f
		};
		f.registerPlugin = function (a, b, c, e) {
			d.plugins.registerPlugin(a, b, c, e)
		};
		f.setPlayer = function (a, b) {
			s = a;
			f.renderingMode = b
		};
		f.detachMedia = function () {
			if ("html5" == f.renderingMode)
				return j("umDetachMedia")
		};
		f.attachMedia = function (a) {
			if ("html5" == f.renderingMode)
				return j("umAttachMedia", a)
		};
		f.dispatchEvent = function (a, b) {
			if (r[a])
				for (var c = h.translateEventResponse(a, b), e = 0; e < r[a].length; e++)
					if ("function" ==
						typeof r[a][e])
						try {
							r[a][e].call(this, c)
						} catch (d) {
							h.log("There was an error calling back an event handler")
						}
		};
		f.dispatchInstreamEvent = function (a) {
			w && w.dispatchEvent(a, arguments)
		};
		f.callInternal = j;
		f.playerReady = function (a) {
			u = !0;
			s || f.setPlayer(b.getElementById(a.id));
			f.container = b.getElementById(f.id);
			h.foreach(r, function (a) {
				n(s, a)
			});
			q(g.UMPLAYER_PLAYLIST_ITEM, function () {
				x = {}

			});
			q(g.UMPLAYER_MEDIA_META, function (a) {
				h.extend(x, a.metadata)
			});
			for (f.dispatchEvent(g.API_READY); 0 < t.length; )
				j.apply(this, t.shift())
		};
		f.getItemMeta = function () {
			return x
		};
		f.getCurrentItem = function () {
			return j("umGetPlaylistIndex")
		};
		return f
	};
	e.selectPlayer = function (c) {
		var d;
		h.exists(c) || (c = 0);
		c.nodeType ? d = c : "string" == typeof c && (d = b.getElementById(c));
		return d ? (c = e.playerById(d.id)) ? c : e.addPlayer(new e(d)) : "number" == typeof c ? a[c] : null
	};
	e.playerById = function (b) {
		for (var c = 0; c < a.length; c++)
			if (a[c].id == b)
				return a[c];
		return null
	};
	e.addPlayer = function (b) {
		for (var c = 0; c < a.length; c++)
			if (a[c] == b)
				return b;
		a.push(b);
		return b
	};
	e.destroyPlayer = function (c) {
		for (var e =
				-1, d, g = 0; g < a.length; g++)
			a[g].id == c && (e = g, d = a[g]);
		0 <= e && (c = d.id, g = b.getElementById(c + ("flash" == d.renderingMode ? "_wrapper" : "")), h.clearCss && h.clearCss("#" + c), g && ("html5" == d.renderingMode && d.destroyPlayer(), d = b.createElement("div"), d.id = c, g.parentNode.replaceChild(d, g)), a.splice(e, 1));
		return null
	};
	d.playerReady = function (a) {
		var b = d.api.playerById(a.id);
		b ? b.playerReady(a) : d.api.selectPlayer(a.id).playerReady(a)
	}
}(umplayer), 

function (d) {
	d.api.instream = function (a, d, g, c) {
		this.play = function (a) {
			d.umInstreamPlay(a)
		};
		this.pause = function (a) {
			d.umInstreamPause(a)
		};
		this.destroy = function () {
			d.umInstreamDestroy()
		};
		a.callInternal("umLoadInstream", g, c ? c : {})
	}
}(umplayer), 

function (d) {
	var a = d.api,
	h = a.selectPlayer;
	a.selectPlayer = function (a) {
		return (a = h(a)) ? a : {
			registerPlugin : function (a, b, e) {
				d.plugins.registerPlugin(a, b, e)
			}
		}
	}
}(umplayer));
