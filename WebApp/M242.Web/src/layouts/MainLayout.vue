<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-banner class="bg-negative text-white" v-if="!tempOk">
        <template v-slot:avatar>
          <q-icon name="signal_wifi_off" />
        </template>
        Die Temperatur ist zu noch zu hoch um sich einzuloggen
      </q-banner>
      <q-toolbar>
        <q-btn
          flat
          dense
          round
          icon="menu"
          aria-label="Menu"
          @click="toggleLeftDrawer"
          v-if="$store.getters['auth/authorized']"
        />

        <q-toolbar-title> M242 </q-toolbar-title>

        <div></div>
      </q-toolbar>
    </q-header>

    <q-drawer
      v-model="leftDrawerOpen"
      show-if-above
      bordered
      v-if="$store.getters['auth/authorized']"
    >
      <q-list>
        <EssentialLink
          v-for="link in essentialLinks"
          :key="link.title"
          v-bind="link"
        />
      </q-list>
    </q-drawer>

    <q-page-container>
      <router-view :tempOk="tempOk" />
    </q-page-container>
  </q-layout>
</template>

<script>
import EssentialLink from "components/EssentialLink.vue";

const linksList = [
  {
    title: "Administration",
    caption: "",
    icon: "manage_accounts",
    link: "Administration",
  },
  {
    title: "Home",
    caption: "",
    icon: "home",
    link: "/",
  },
];

import { defineComponent, ref } from "vue";

export default defineComponent({
  name: "MainLayout",
  data() {
    return {
      tempOk: false,
      leftDrawerOpen: false,
      essentialLinks: linksList,
    };
  },
  mounted() {
    this.getTemp();
    setInterval(this.getTemp, 30000);
  },
  components: {
    EssentialLink,
  },

  methods: {
    toggleLeftDrawer() {
      leftDrawerOpen.value = !leftDrawerOpen.value;
    },
    async getTemp() {
      let res = await axios.get("Home/CheckTemp");
      if (res.status === 200 && res.data == "") this.tempOk = true;
      else this.tempOk = false;
    },
  },
});
</script>
