mergeInto(LibraryManager.library, {
  MessageToPlayer: function (userName, message) {
    window.dispatchReactUnityEvent("MessageToPlayer", UTF8ToString(userName), UTF8ToString(message));
  },
});

mergeInto(LibraryManager.library, {
  MessageToAllPlayers: function (message) {
    window.dispatchReactUnityEvent("MessageToAllPlayers", UTF8ToString(message));
  },
});

mergeInto(LibraryManager.library, {
  Exit: function () {
    window.dispatchReactUnityEvent("Exit");
  },
});

mergeInto(LibraryManager.library, {
  UpdateStats: function (userName, stats) {
    window.dispatchReactUnityEvent("UpdateStats", UTF8ToString(userName), UTF8ToString(stats));
  },
});
