<template>
  <q-page>
    <div class="q-ma-md row q-gutter-md">
      <q-card class="col">
        <q-card-section v-if="TreeMapNFCRegistersSeries">
          <apexchart
            type="treemap"
            :options="TreeMapNFCRegistersOptions"
            :series="TreeMapNFCRegistersSeries"
          ></apexchart>
        </q-card-section>

        <q-separator dark />
      </q-card>
      <q-card class="col">
        <q-card-section v-if="TodaysTempSeries">
          <apexchart
            type="line"
            :options="TodaysTempOptions"
            :series="TodaysTempSeries"
          ></apexchart>
        </q-card-section>

        <q-separator dark />
      </q-card>
    </div>
    <div class="q-ma-md row q-gutter-md">
      <q-card class="col">
        <q-card-section v-if="TempNearCriticalPointSeries">
          <apexchart
            type="radialBar"
            :options="TempNearCriticalPointOptions"
            :series="TempNearCriticalPointSeries"
            ref="TempNearCriticalPoint"
          ></apexchart>
        </q-card-section>

        <q-separator dark />
      </q-card>
      <div class="col"></div>
    </div>
  </q-page>
</template>

<script>
import { defineComponent } from "vue";

export default defineComponent({
  name: "Administration",
  data() {
    return {
      //TreeMap
      TreeMapNFCRegistersOptions: {
        plotOptions: {
          treemap: {
            distributed: true,
            enableShades: false,
          },
        },
        colors: [
          "#3B93A5",
          "#0958d9",
          "#e60760",
          "#e69807",
          "#07e62c",
          "#07e62c",
        ],
        title: {
          text: "NFCRegisters Treemap",
          align: "center",
        },
        chart: {
          id: "TreeMapNFCRegisters",
        },
      },
      TreeMapNFCRegistersSeries: null,

      //Line
      TodaysTempOptions: {
        title: {
          text: "Temp / Hum Line Chart",
          align: "center",
        },
        chart: {
          id: "TodaysTemp",
        },
        xaxis: { categories: null },
      },
      TodaysTempSeries: null,

      //Stroked Gauge
      TempNearCriticalPointOptions: {
        title: {
          text: "",
          align: "center",
        },
        chart: {
          id: "TempNearCriticalPoint",
        },
        plotOptions: {
          radialBar: {
            startAngle: -135,
            endAngle: 135,
            dataLabels: {
              name: {
                fontSize: "16px",
                offsetY: 120,
              },
              value: {
                offsetY: 76,
                fontSize: "22px",
                formatter: function (val) {
                  return val + "%";
                },
              },
            },
          },
        },
        fill: {
          type: "gradient",
          gradient: {
            shade: "reds",
            stops: [0],
          },
        },
        colors: [
          function ({ value, seriesIndex, w }) {
            if (value < 80) return "#0fe800";
            else if (value >= 100) return "red";
            else return "#ffa600";
          },
        ],
        labels: ["Temperature"],
      },
      TempNearCriticalPointSeries: null,
    };
  },
  mounted() {
    this.getTreeMapNFCRegistersSeries();
    this.getTodaysTemp();
    setInterval(this.GetTempNearCriticalPoint(), 30000);
  },
  computed: {},
  methods: {
    async GetTempNearCriticalPoint() {
      console.log(this.$refs.apexChart);
      let res = await axios.get("Auswertungen/GetTempNearCriticalPoint");
      if (res.status === 200) {
        this.TempNearCriticalPointSeries = [res.data];
      }
    },
    async getTodaysTemp() {
      let res = await axios.get("Auswertungen/GetTodaysTempData");
      if (res.status === 200) {
        this.TodaysTempOptions.xaxis.categories = res.data.map((x) => x.time);
        this.TodaysTempSeries = [
          {
            name: "Temperature",
            data: res.data.map((x) => x.temp),
          },
          {
            name: "Humidity",
            data: res.data.map((x) => x.hum),
          },
        ];
      }
    },
    async getTreeMapNFCRegistersSeries() {
      let res = await axios.get("Auswertungen/TreeMapNFCRegisters");
      if (res.status === 200) {
        this.TreeMapNFCRegistersSeries = res.data;
      }
    },
  },
});
</script>
