<template>
  <q-page>
    <q-splitter v-model="splitterModel" style="min-height: 93vh">
      <template v-slot:before>
        <q-tabs v-model="tab" vertical class="text-primary">
          <q-tab name="login" icon="login" label="Login" disable />
          <q-tab name="nfc" icon="nfc" label="NFC" disable />
        </q-tabs>
      </template>

      <template v-slot:after>
        <q-tab-panels
          v-model="tab"
          animated
          swipeable
          vertical
          transition-prev="jump-up"
          transition-next="jump-up"
        >
          <q-tab-panel
            name="login"
            class="flex flex-center"
            style="min-height: 93vh"
          >
            <div>
              <div class="text-h3 text-center">Login</div>
              <q-input
                outlined
                v-model="email"
                label="E-mail"
                class="q-my-sm"
              />
              <q-input
                outlined
                v-model="password"
                label="Password"
                class="q-my-sm"
              />
              <q-btn
                color="white"
                text-color="black"
                label="Login"
                class="float-right"
                @click="login"
                :disable="!tempOk"
              />
            </div>
          </q-tab-panel>

          <q-tab-panel
            name="nfc"
            class="flex flex-center"
            style="min-height: 93vh"
          >
            <div>
              <q-icon
                name="nfc"
                size="203px"
                color="primary"
                style="margin: 0 auto; display: block"
              /><br />
              <span class="text-h4 text-weight-light">Bitte Karte Scanen</span>
            </div>
          </q-tab-panel>
        </q-tab-panels>
      </template>
    </q-splitter>
  </q-page>
</template>

<script>
import { Notify } from "quasar";
export default {
  name: "PageIndex",
  data() {
    return {
      email: null,
      password: null,
      tab: "login",
      uid: null,
    };
  },
  mounted() {},
  computed: {
    tempOk() {
      return this.$attrs.tempOk;
    },
  },
  methods: {
    async login() {
      let res = await axios.post("Authentication/Login", {
        Email: this.email,
        Password: this.password,
      });
      if (res.status !== 200) {
        this.tab = "login";
        Notify.create({
          message: "Password or Email is Wrong",
          color: "negative",
        });
        return;
      }
      this.uid = res.data.id;
      let date = res.data.date;
      this.tab = "nfc";
      do {
        if (!this.tempOk) {
          Notify.create({
            message: "Login Nicht möglich wegen zu hoher Temperatur",
            color: "negative",
          });
          this.tab = "login";
          return;
        }
        res = await axios.get("authentication/NFCLogin", {
          params: {
            id: this.uid,
            date: date,
          },
        });
        await new Promise((r) => setTimeout(r, 1000));
      } while (res.data != true);
      this.$router.push("Administration");
    },
  },
};
</script>
